using Domain.Models;

namespace Application.Contracts.RepositoryContracts;

public interface ICategoryRepository : IRepositoryBase<Category>
{
    Task<bool> IsUniqueNameAsync(string name, CancellationToken cancellationToken);
    Task<Category?> TryGetByNameAsync(string name, CancellationToken cancellationToken);
    Task<Category?> TryGetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Category>> GetAllAsync(CancellationToken cancellationToken);
}