namespace Application.Contracts.ImageServiceContracts;

public interface IImageService
{
    public Task<(byte[] fileBytes, string contentType)> GetImageAsync
        (string fileName, string imageStoragePath);
    public Task WriteFileAsync(IFormFile image, string filePath);
    public void DeleteFile(string filePath);
    ImageUrlConfiguration InitializeImageUrlConfiguration(HttpContext httpContext, string controllerRoute,
        string imageEndpointRoute);
    string InitializeImageStoragePath(IConfiguration configuration, IWebHostEnvironment hostingEnvironment);

}