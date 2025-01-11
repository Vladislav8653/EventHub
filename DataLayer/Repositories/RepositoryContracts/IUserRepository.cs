using DataLayer.Models;

namespace DataLayer.Repositories.RepositoryContracts;

public interface IUserRepository : IRepositoryBase<User>
{
    Task<User?> GetUserByLoginAsync(string login);
}