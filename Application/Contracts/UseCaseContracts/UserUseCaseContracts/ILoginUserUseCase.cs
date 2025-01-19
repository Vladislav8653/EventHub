using Application.DtoModels.UserDto;

namespace Application.Contracts.UseCaseContracts.UserUseCaseContracts;

public interface ILoginUserUseCase
{
    Task<LoginUserResponse> Handle(LoginUserRequest request);
}