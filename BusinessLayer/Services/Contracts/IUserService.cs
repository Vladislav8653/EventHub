using BusinessLayer.DtoModels.UserDto;

namespace BusinessLayer.Services.Contracts;

public interface IUserService
{
    Task Register(RegisterUserRequest request);
    Task<string> Login(LoginUserRequest request);
}