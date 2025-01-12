using Domain.Models;
using Infrastructure.Repositories.RepositoryContracts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.RepositoriesImplementations;


public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
{
    public CategoryRepository(EventHubDbContext eventHubDbContext) : base(eventHubDbContext) { }

    public async Task<bool> IsUniqueNameAsync(string name) =>
        !await Repository.Categories.AnyAsync(c => c.Name == name);
    
    public async Task<Category?> TryGetByNameAsync(string name) => 
        await Repository.Categories.FirstOrDefaultAsync(c => c.Name == name);

    public async Task<Category?> TryGetByIdAsync(Guid id) =>
        await Repository.Categories.FirstOrDefaultAsync(c => c.Id == id);

    public async Task<IEnumerable<Category>> GetAllAsync() =>
        await Repository.Categories.ToListAsync();
}