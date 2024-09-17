using ThumbnailGenerator.Models;

namespace ThumbnailGenerator.Abstractions;

public interface IThumbnailService
{
    Task<GenerateThumbnailResult> GenerateThumbnail(string filePath);
}