namespace ThumbnailGenerator.Services;

public partial class ThumbnailService : IThumbnailService
{
    public async Task<GenerateThumbnailResult> GenerateThumbnail(string filePath)
    {
        try
        {
            throw new PlatformNotSupportedException("Thumbnail generation is not supported by this platform");
        }
        catch (Exception ex)
        {
            var failure = new OperationFailed(ex);

            return new GenerateThumbnailResult(failure);
        }
    }
}