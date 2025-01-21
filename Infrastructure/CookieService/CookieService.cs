using Application.Contracts;

namespace Infrastructure.CookieService;

public class CookieService : ICookieService
{
    public void AddCookie(HttpResponse response, string cookieName, string? content)
    {
        if (content == null)
            return;
        var options = new CookieOptions
        {
            HttpOnly = true, // Защита от JavaScript
            Secure = true,   // Токены должны передаваться только по HTTPS
            SameSite = SameSiteMode.Strict // Защита от CSRF-атак
        };

        response.Cookies.Append(cookieName, content, options);
    }

    public string? GetCookie(HttpRequest request, string cookieName)
    {
        return request.Cookies[cookieName];
    }

    public void DeleteCookie(HttpResponse response, string cookieName)
    {
        response.Cookies.Delete(cookieName); 
    }
}