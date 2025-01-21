using Domain.Models;

namespace Domain.RepositoryContracts;

public interface IUserRepository : IRepositoryBase<User>
{
    Task<User?> GetUserByLoginAsync(string login, CancellationToken cancellationToken);
    Task<User?> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken);
}