namespace Application.Contracts.ImageServiceContracts;

public class ImageUrlConfiguration(HttpRequest request, string controllerName, string endpointName)
{
    public string BaseUrl { get; } = $"{request.Scheme}://{request.Host}";
    public string ControllerRoute { get; } = controllerName;
    public string EndpointRoute { get; } = endpointName;
}