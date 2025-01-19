using Application.Contracts.RepositoryContracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.RepositoryImplementations;

public class UserRepository : RepositoryBase<User>, IUserRepository
{
    public UserRepository(EventHubDbContext repositoryContext) : base(repositoryContext) { }
    public async Task<User?> GetUserByLoginAsync(string login) => 
        await Repository.Users.FirstOrDefaultAsync(u => u.Login == login);

    public async Task<User?> GetUserByIdAsync(Guid userId) =>
        await Repository.Users.FirstOrDefaultAsync(u => u.Id == userId);
}