using Domain.Models;

namespace Application.Contracts.RepositoryContracts;

public interface ICategoryRepository : IRepositoryBase<Category>
{
    Task<bool> IsUniqueNameAsync(string name);
    Task<Category?> TryGetByNameAsync(string name);
    Task<Category?> TryGetByIdAsync(Guid id);
    Task<IEnumerable<Category>> GetAllAsync();
}