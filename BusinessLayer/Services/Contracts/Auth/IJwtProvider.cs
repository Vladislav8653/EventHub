using DataLayer.Models;

namespace BusinessLayer.Services.Contracts.Auth;

public interface IJwtProvider
{
    string GenerateToken(User user);
}