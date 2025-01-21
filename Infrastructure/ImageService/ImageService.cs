using Application.Contracts;
using Application.Contracts.ImageServiceContracts;
using Application.Exceptions;

namespace Infrastructure.ImageService;

public class ImageService : IImageService
{
    private const string ImageStoragePath = "ImageStorage:wwwrootRelativePath";
    
    public async Task<(byte[] fileBytes, string contentType)> GetImageAsync
        (string fileName, string imageStoragePath)
    {
        var filePath = Path.Combine(imageStoragePath, fileName);
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

    public ImageUrlConfiguration InitializeImageUrlConfiguration(HttpContext httpContext, string controllerRoute, string imageEndpointRoute)
    {
        if (httpContext == null)
            throw new InvalidOperationException("HttpContext is not available");
        var request = httpContext.Request;
        return new ImageUrlConfiguration(request, controllerRoute, imageEndpointRoute);
    }

    public string InitializeImageStoragePath(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
    {
        var config = configuration[ImageStoragePath];
        if (config == null)
            throw new InvalidOperationException("Image storage path is not available.");
        var imagePath = Path.Combine(hostingEnvironment.WebRootPath, config);
        return imagePath;
    }
}