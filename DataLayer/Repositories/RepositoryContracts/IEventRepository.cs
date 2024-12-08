using DataLayer.Models;
using DataLayer.Specifications.Dto;

namespace DataLayer.Repositories.RepositoryContracts;

public interface IEventRepository : IRepositoryBase<Event>
{
    Task<bool> IsUniqueNameAsync(string name);
    Task<Event?> GetByNameAsync(string name);
    Task<Event?> GetByIdAsync(Guid id);
    Task<(IEnumerable<Event>, int)> GetAllByParamsAsync(EventQueryParams eventParams);
    Task<IEnumerable<Event>> GetAllUserEventsAsync (Guid userId);
}