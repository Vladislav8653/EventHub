using DataLayer.Models;

namespace DataLayer.Repositories.RepositoryContracts;

public interface IUserRepository : IRepositoryBase<User>
{
    Task<bool> IsUniqueLoginAsync(string login);
    Task<User?> GetUserByLoginAsync(string login);
}