namespace Application.Contracts;

public interface IImageService
{
    public Task<(byte[] fileBytes, string contentType)> GetImageAsync
        (string fileName, string relativeFilePath);
    public Task WriteFileAsync(IFormFile image, string filePath);
    public void DeleteFile(string filePath);
}