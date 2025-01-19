namespace Application.Contracts;

public interface ICookieService
{
    void AddCookie(HttpResponse response, string cookieName, string? content);
    string? GetCookie(HttpRequest request, string cookieName);
    void DeleteCookie(HttpResponse response, string cookieName);
}