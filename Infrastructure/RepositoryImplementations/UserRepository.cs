using Domain.RepositoryContracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.RepositoryImplementations;

public class UserRepository : RepositoryBase<User>, IUserRepository
{
    public UserRepository(EventHubDbContext repositoryContext) : base(repositoryContext) { }
    public async Task<User?> GetUserByLoginAsync(string login, CancellationToken cancellationToken) => 
        await Repository.Users.FirstOrDefaultAsync(u => u.Login == login, cancellationToken);

    public async Task<User?> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken) =>
        await Repository.Users.FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
}