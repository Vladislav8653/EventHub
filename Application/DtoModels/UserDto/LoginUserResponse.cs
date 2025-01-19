namespace Application.DtoModels.UserDto;

public class LoginUserResponse
{
    public LoginUserResponse(string message, string accessToken = "", string refreshToken = "")
    {
        Message = message;
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }
    public string Message { get; set; }
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}