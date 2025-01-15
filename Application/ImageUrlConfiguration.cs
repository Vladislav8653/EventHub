namespace Application;

public class ImageUrlConfiguration(HttpRequest request)
{
    public string BaseUrl { get; } = $"{request.Scheme}://{request.Host}";
}