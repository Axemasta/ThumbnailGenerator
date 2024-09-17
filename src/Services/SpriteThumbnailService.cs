namespace ThumbnailGenerator.Services;

// Testing the image source code further down the pipe works
public class SpriteThumbnailService : IThumbnailService
{
    public async Task<GenerateThumbnailResult> GenerateThumbnail(string filePath)
    {
        try
        {
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync("https://1000logos.net/wp-content/uploads/2020/09/Sprite-Logo.png"))
                {
                    byte[] imageBytes = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
                    
                    var result = new ThumbnailGenerated(imageBytes);
           
                    return new GenerateThumbnailResult(result);
                }
            }
        }
        catch (Exception ex)
        {
            var failure = new OperationFailed(ex);

            return new GenerateThumbnailResult(failure);
        }
    }
}