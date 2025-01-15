namespace Application;

public class ImageUrlConfiguration(HttpRequest request)
{
    public string BaseUrl { get; init; } = $"{request.Scheme}://{request.Host}";
}