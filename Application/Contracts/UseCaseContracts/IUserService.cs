using Application.DtoModels.EventsDto;
using Application.DtoModels.UserDto;

namespace Application.Contracts.UseCaseContracts;

public interface IUserService
{
    Task<RegisterUserResponse> Register(RegisterUserRequest request);
    Task<LoginUserResponse> Login(LoginUserRequest request);
    Task<IEnumerable<GetEventDto>> GetAllUserEventsAsync(HttpRequest request, string userIdStr);
    
}