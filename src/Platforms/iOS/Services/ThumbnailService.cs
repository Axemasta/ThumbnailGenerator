using AVFoundation;
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
            var image = GetVideoThumbnail(filePath);

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
    private UIImage? GetVideoThumbnail(string path)
    {
        try 
        {
            CMTime actualTime;
            NSError outError;
            using (var asset = AVAsset.FromUrl (NSUrl.FromFilename (path)))
            using (var imageGen = new AVAssetImageGenerator (asset){ AppliesPreferredTrackTransform = true}) // https://stackoverflow.com/questions/5347800/avassetimagegenerator-provides-images-rotated
            using (var imageRef = imageGen.CopyCGImageAtTime (new CMTime (1, 1), out actualTime, out outError)) {
                return UIImage.FromImage (imageRef);
            }	
        } 
        catch (Exception ex)
        {
            Console.WriteLine("An exception occurred getting native thumbnail");
            Console.WriteLine(ex);
            return null;
        }
    }
}