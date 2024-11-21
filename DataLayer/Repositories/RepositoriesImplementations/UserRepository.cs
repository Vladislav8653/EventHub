using DataLayer.Data;
using DataLayer.Models;
using DataLayer.Repositories.RepositoryContracts;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories.RepositoriesImplementations;

public class UserRepository : RepositoryBase<User>, IUserRepository
{
    public UserRepository(EventHubDbContext repositoryContext) : base(repositoryContext) { }

    public async Task<bool> IsUniqueLoginAsync(string login) => 
        !await Repository.Users.AnyAsync(u => u.Login == login);


    public async Task<User?> GetUserByLoginAsync(string login) => 
        await Repository.Users.FirstOrDefaultAsync(u => u.Login == login);
}