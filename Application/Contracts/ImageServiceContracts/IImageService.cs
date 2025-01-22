using Domain.Models;

namespace Application.Contracts.ImageServiceContracts;

public interface IImageService
{
    Task<(byte[] fileBytes, string contentType)> GetImageAsync
        (string fileName);
    Task WriteFileAsync(IFormFile image);
    void DeleteFile(string fileName);
    ImageUrlConfiguration InitializeImageUrlConfiguration(HttpContext httpContext, string controllerRoute, string imageEndpointRoute);
    List<Event> AttachLinkToImage(IEnumerable<Event> items, ImageUrlConfiguration request);
    Event AttachLinkToImage(Event item, ImageUrlConfiguration request);

}