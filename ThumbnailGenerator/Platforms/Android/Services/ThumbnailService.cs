using Android.Graphics;
using Android.Media;
using Android.Provider;

namespace ThumbnailGenerator.Services;

public partial class ThumbnailService
{
    public async Task<GenerateThumbnailResult> GenerateThumbnail(string filePath)
    {
        try
        {
            var bitmap = await ThumbnailUtils.CreateVideoThumbnailAsync(filePath, ThumbnailKind.MiniKind);

            if (bitmap is null)
            {
                throw new FileNotFoundException($"Could not load thumbnail from file path: {filePath}");
            }
            
            using var stream = new MemoryStream();
            var success = await bitmap.CompressAsync(Bitmap.CompressFormat.Jpeg, 75, stream);

            if (!success)
            {
                throw new InvalidOperationException("Could not turn thumbnail bitmap into jpeg");
            }

            var generatedThumbnail = new ThumbnailGenerated(stream.ToArray());
            return new GenerateThumbnailResult(generatedThumbnail);
        }
        catch (Exception ex)
        {
            var failure = new OperationFailed(ex);

            return new GenerateThumbnailResult(failure);
        }
    }
}