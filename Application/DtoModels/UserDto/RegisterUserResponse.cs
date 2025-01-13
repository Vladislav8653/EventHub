namespace Application.DtoModels.UserDto;

public class RegisterUserResponse
{
    public RegisterUserResponse(string message)
    {
        Message = message;
    }
    public string Message { get; set; }
}