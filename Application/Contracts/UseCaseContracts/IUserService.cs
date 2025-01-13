using BusinessLayer.DtoModels.EventsDto;
using BusinessLayer.DtoModels.UserDto;
using Microsoft.AspNetCore.Http;

namespace BusinessLayer.Services.Contracts;

public interface IUserService
{
    Task<RegisterUserResponse> Register(RegisterUserRequest request);
    Task<LoginUserResponse> Login(LoginUserRequest request);
    Task<IEnumerable<GetEventDto>> GetAllUserEventsAsync(HttpRequest request, string userIdStr);
    
}