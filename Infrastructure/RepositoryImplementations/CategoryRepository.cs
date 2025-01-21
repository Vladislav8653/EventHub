using Domain.RepositoryContracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.RepositoryImplementations;


public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
{
    public CategoryRepository(EventHubDbContext eventHubDbContext) : base(eventHubDbContext) { }

    public async Task<bool> IsUniqueNameAsync(string name, CancellationToken cancellationToken) =>
        !await Repository.Categories.AnyAsync(c => c.Name == name, cancellationToken);
    
    public async Task<Category?> TryGetByNameAsync(string name, CancellationToken cancellationToken) => 
        await Repository.Categories.FirstOrDefaultAsync(c => c.Name == name, cancellationToken);

    public async Task<Category?> TryGetByIdAsync(Guid id, CancellationToken cancellationToken) =>
        await Repository.Categories.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

    public async Task<IEnumerable<Category>> GetAllAsync(CancellationToken cancellationToken) =>
        await Repository.Categories.ToListAsync(cancellationToken);
}