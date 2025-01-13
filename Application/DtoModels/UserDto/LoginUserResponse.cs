namespace Application.DtoModels.UserDto;

public class LoginUserResponse
{
    public LoginUserResponse(string message, string token = "")
    {
        Message = message;
        Token = token;
    }
    public string Message { get; set; }
    public string Token { get; set; }
}