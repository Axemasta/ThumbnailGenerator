using AVFoundation;
using CoreGraphics;
using CoreMedia;
using Foundation;
using UIKit;

namespace ThumbnailGenerator.Services;

public partial class ThumbnailService
{
    public async Task<GenerateThumbnailResult> GenerateThumbnail(string filePath)
    {
        try
        {
            var image = await GetVideoThumbnail(filePath);

            if (image is null)
            {
                return new GenerateThumbnailResult(new OperationFailed(new FileNotFoundException($"Unable to load file as UIImage: {filePath}")));
            }

            // https://stackoverflow.com/questions/17112314/converting-uiimage-to-byte-array
            var jpg = image.AsJPEG();

            if (jpg is null)
            {
                return new GenerateThumbnailResult(new OperationFailed(new FileNotFoundException($"Unable to load file as JPG: {filePath}")));
            }
            
            var memoryStream = new MemoryStream();
            await jpg.AsStream().CopyToAsync(memoryStream);
            
            var generatedThumbnail = new ThumbnailGenerated(memoryStream.ToArray());

            return new GenerateThumbnailResult(generatedThumbnail);
        }
        catch (Exception ex)
        {
            var failure = new OperationFailed(ex);

            return new GenerateThumbnailResult(failure);
        }
    }

    // https://gist.github.com/dannycabrera/7f83f168b0e07311ee0d
    private async Task<UIImage?> GetVideoThumbnail(string path)
    {
        try
        {
            var tcs = new TaskCompletionSource<UIImage?>();
            
            var nsUrl = NSUrl.FromFilename(path);
            
            using var asset = AVAsset.FromUrl (nsUrl);
            
            using var imageGenerator = new AVAssetImageGenerator (asset);
            imageGenerator.AppliesPreferredTrackTransform = true;
            
            // https://github.com/xamarin/xamarin-macios/issues/18452
            imageGenerator.GenerateCGImagesAsynchronously(new[] { NSValue.FromCMTime(new CMTime(1, 1)) }, (requestedTime, imageRef, actualTime, result, error) =>
            {
                if (error != null)
                {
                    tcs.SetException(new Exception(error.LocalizedDescription));
                }
                
                var image = UIImage.FromImage(imageRef);
                
                tcs.SetResult(image);
            });

            return await tcs.Task;
        } 
        catch (Exception ex)
        {
            Console.WriteLine("An exception occurred getting native thumbnail");
            Console.WriteLine(ex);
            return null;
        }
    }
}