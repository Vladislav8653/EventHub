using Application.Contracts;
using Application.Exceptions;

namespace Application.FileManager;

public class ImageService : IImageService
{
    private readonly IWebHostEnvironment _hostingEnvironment;

    public ImageService(IWebHostEnvironment hostingEnvironment)
    {
        _hostingEnvironment = hostingEnvironment;
    }
    
    public async Task<(byte[] fileBytes, string contentType)> GetImageAsync
        (string fileName, string relativeFilePath)
    {
        var filePath = Path.Combine(_hostingEnvironment.WebRootPath, relativeFilePath, fileName);
        if (!File.Exists(filePath))
        {
            throw new EntityNotFoundException("Image is not found.");
        }
        var fileBytes = await File.ReadAllBytesAsync(filePath);
        var contentType = $"image/{Path.GetExtension(fileName).TrimStart('.').ToLowerInvariant()}";
        return (fileBytes, contentType);
    }
    
    public async Task WriteFileAsync(IFormFile image, string filePath)
    {
        await using var stream = new FileStream(filePath, FileMode.Create);
        await image.CopyToAsync(stream);
    }

    public void DeleteFile(string filePath)
    {
        File.Delete(filePath);
    }    

}