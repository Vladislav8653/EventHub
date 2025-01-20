using Application.DtoModels.UserDto;

namespace Application.Contracts.UseCaseContracts.UserUseCaseContracts;

public interface IRegisterUserUseCase
{
    Task<RegisterUserResponse> Handle(RegisterUserRequest request, CancellationToken cancellationToken);
}