namespace ThumbnailGenerator.Models;

public class GenerateThumbnailResult(OneOf<ThumbnailGenerated, OperationFailed> input) 
    : OneOfBase<ThumbnailGenerated, OperationFailed>(input)
{
    public bool IsThumbnailGenerated => IsT0;
    
    public bool IsOperationFailed => IsT1;

    public ThumbnailGenerated ThumbnailGenerated => AsT0;
    
    public OperationFailed OperationFailed => AsT1;
}

public record ThumbnailGenerated(byte[] ThumbnailBytes);

public record OperationFailed(Exception Exception);