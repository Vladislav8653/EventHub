namespace Application.DtoModels.UserDto;

public class RegisterUserRequest
{
    public string UserName { get; set; } = string.Empty;
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}