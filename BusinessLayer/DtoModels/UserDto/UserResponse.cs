namespace BusinessLayer.DtoModels.UserDto;

public class UserResponse
{
    public UserResponse(string message)
    {
        Message = message;
    }
    public string Message { get; set; }
}