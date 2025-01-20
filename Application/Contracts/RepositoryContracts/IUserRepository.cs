using Domain.Models;

namespace Application.Contracts.RepositoryContracts;

public interface IUserRepository : IRepositoryBase<User>
{
    Task<User?> GetUserByLoginAsync(string login, CancellationToken cancellationToken);
    Task<User?> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken);
}