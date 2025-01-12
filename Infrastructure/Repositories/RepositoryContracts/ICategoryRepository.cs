using Domain.Models;

namespace Infrastructure.Repositories.RepositoryContracts;

public interface ICategoryRepository : IRepositoryBase<Category>
{
    Task<bool> IsUniqueNameAsync(string name);
    Task<Category?> TryGetByNameAsync(string name);
    Task<Category?> TryGetByIdAsync(Guid id);
    Task<IEnumerable<Category>> GetAllAsync();
}