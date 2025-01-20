namespace Application.Contracts.UseCaseContracts.UserUseCaseContracts;

public interface IRefreshTokenUseCase
{
    Task<string> Handle(string? accessToken, string? refreshToken);
}