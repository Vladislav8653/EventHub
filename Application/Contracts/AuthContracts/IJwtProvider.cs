using System.Security.Claims;
using Domain.Models;

namespace Application.Contracts.AuthContracts;

public interface IJwtProvider
{
    string GenerateAccessToken(User user);
    string GenerateRefreshToken();
    ClaimsPrincipal GetClaimsAccessToken(string token);
    bool ValidateRefreshToken(string? originalTrueToken, string userToken, DateTime? expiresTime);
    Guid GetUserIdAccessToken(string? token);
}