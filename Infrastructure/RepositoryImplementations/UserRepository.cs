using Application.Contracts.RepositoryContracts;
using Domain.Models;
using Infrastructure.RepositoryImplementations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.RepositoriesImplementations;

public class UserRepository : RepositoryBase<User>, IUserRepository
{
    public UserRepository(EventHubDbContext repositoryContext) : base(repositoryContext) { }
    public async Task<User?> GetUserByLoginAsync(string login) => 
        await Repository.Users.FirstOrDefaultAsync(u => u.Login == login);
    
}