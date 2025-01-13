using Domain.Models;

namespace Application.Contracts.AuthContracts;

public interface IJwtProvider
{
    string GenerateToken(User user);
}