using BusinessLayer.DtoModels.UserDto;

namespace BusinessLayer.Services.Contracts;

public interface IUserService
{
    Task<UserResponse> Register(RegisterUserRequest request);
    Task<UserResponse> Login(LoginUserRequest request);
}