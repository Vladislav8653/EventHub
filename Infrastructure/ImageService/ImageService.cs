using Application.Contracts.ImageServiceContracts;
using Application.Exceptions;
using Domain.Models;
using Microsoft.Extensions.Options;

namespace Infrastructure.ImageService;

public class ImageService : IImageService
{
    private readonly string _imageStoragePath;
    public ImageService(IOptions<ImageStorageSettings> imageStorageSettings)
    {
        _imageStoragePath = imageStorageSettings.Value.Path;
    }
    
    public async Task<(byte[] fileBytes, string contentType)> GetImageAsync (string fileName)
    {
        var filePath = Path.Combine(GetImageStoragePath(), fileName);
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
    
    public string GetImageStoragePath()
    {
        return _imageStoragePath;
    }
    
    public List<Event> AttachLinkToImage(IEnumerable<Event> items, ImageUrlConfiguration request)
    {
        var itemsList = items.ToList();
        foreach (var item in itemsList.Where(item => !string.IsNullOrEmpty(item.Image)))
        {
            item.Image = new Uri($"{request.BaseUrl}/{request.ControllerRoute}/{request.EndpointRoute}/{item.Image}").ToString();
        }
        return itemsList;
    }
    
    public Event AttachLinkToImage(Event item, ImageUrlConfiguration request)
    {
        if (!string.IsNullOrEmpty(item.Image)) 
            item.Image = new Uri($"{request.BaseUrl}/{request.ControllerRoute}/{request.EndpointRoute}/{item.Image}").ToString();
        return item;
    }
}