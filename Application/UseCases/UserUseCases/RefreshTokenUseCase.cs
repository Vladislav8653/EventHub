using System.Security.Claims;
using Application.Contracts.AuthContracts;
using Application.Contracts.UseCaseContracts.UserUseCaseContracts;
using Application.Exceptions;
using Domain;
using Domain.RepositoryContracts;

namespace Application.UseCases.UserUseCases;

public class RefreshTokenUseCase : IRefreshTokenUseCase
{
    private readonly IJwtProvider _jwtProvider;
    private readonly IRepositoriesManager _repositoriesManager;
    public RefreshTokenUseCase(IJwtProvider jwtProvider, IRepositoriesManager repositoriesManager)
    {
        _jwtProvider = jwtProvider;
        _repositoriesManager = repositoriesManager;
    }

    public async Task<string> Handle(string? accessToken, string? refreshToken, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(refreshToken))
        {
            throw new ArgumentNullException(nameof(refreshToken), "User does not have refresh token");
        }
        if (string.IsNullOrEmpty(accessToken))
        {
            throw new ArgumentNullException(nameof(accessToken), "User does not have an access token");
        }
        var claims = _jwtProvider.GetClaimsAccessToken(accessToken);
        var userId = claims.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            throw new UnauthorizedAccessException("Invalid token, can't extract user ID.");
        if (!Guid.TryParse(userId, out var userIdGuid))
            throw new InvalidDataTypeException("Can't parse user ID to type Guid.");
        var user = await _repositoriesManager.Users.GetUserByIdAsync(userIdGuid, cancellationToken);
        if (user == null)
            throw new EntityNotFoundException("User", "id", userId);
        _jwtProvider.ValidateRefreshToken(user.RefreshToken, refreshToken, user.RefreshTokenExpireTime);
        return _jwtProvider.GenerateAccessToken(user);
    }
}