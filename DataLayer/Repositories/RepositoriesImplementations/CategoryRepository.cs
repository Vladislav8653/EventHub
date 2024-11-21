using DataLayer.Repositories.RepositoryContracts;
using DataLayer.Data;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories.RepositoriesImplementations;


public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
{
    public CategoryRepository(EventHubDbContext eventHubDbContext) : base(eventHubDbContext) { }

    public async Task<bool> IsUniqueNameAsync(string name) =>
        !await Repository.Categories.AnyAsync(c => c.Name == name);
    
    
    public async Task<Category?> TryGetByNameAsync(string name) => 
        await FindByCondition(c => c.Name == name, trackChanges: false).FirstOrDefaultAsync();
    

    public Category? TryGetById(Guid id) =>
        FindByCondition(c => c.Id== id, trackChanges: false).FirstOrDefault();

    public async Task<IEnumerable<Category>> GetAllAsync() =>
        await FindAll(trackChanges: false).ToListAsync();

    public bool Exists(Guid id) => 
        FindByCondition(c => c.Id == id, trackChanges: false).FirstOrDefault() != null;
}