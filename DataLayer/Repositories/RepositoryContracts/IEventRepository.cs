using DataLayer.Models;

namespace DataLayer.Repositories.RepositoryContracts;

public interface IEventRepository : IRepositoryBase<Event>
{
    Task<bool> IsUniqueNameAsync(string name);
    Task<Event?> GetByNameAsync(string name);
    Task<Event?> GetByIdAsync(Guid id);
    Task<IEnumerable<Event>> GetAllAsync();
    Task<IEnumerable<Event>> GetByDateAsync(DateTimeOffset date);
    Task<IEnumerable<Event>> GetByDateRangeAsync(DateTimeOffset start, DateTimeOffset finish);
    Task<IEnumerable<Event>> GetByCategoryAsync(Category category);
    Task<IEnumerable<Event>> GetByPlaceAsync(string place);
}