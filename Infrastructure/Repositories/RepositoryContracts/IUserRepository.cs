using Domain.Models;

namespace Infrastructure.Repositories.RepositoryContracts;

public interface IUserRepository : IRepositoryBase<User>
{
    Task<User?> GetUserByLoginAsync(string login);
}