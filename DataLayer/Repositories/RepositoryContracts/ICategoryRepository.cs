using DataLayer.Models;

namespace DataLayer.Repositories.RepositoryContracts;

public interface ICategoryRepository : IRepositoryBase<Category>
{
    Task<bool> IsUniqueNameAsync(string name);
    Task<Category?> TryGetByNameAsync(string name);
    Category? TryGetById(Guid id);
    Task<IEnumerable<Category>> GetAllAsync();
    bool Exists(Guid id);


}