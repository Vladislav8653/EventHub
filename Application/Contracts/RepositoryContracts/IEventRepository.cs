using Application.Specifications.Dto;
using Domain.Models;

namespace Application.Contracts.RepositoryContracts;

public interface IEventRepository : IRepositoryBase<Event>
{
    Task<bool> IsUniqueNameAsync(string name);
    Task<Event?> GetByNameAsync(string name);
    Task<Event?> GetByIdAsync(Guid id);
    Task<(IEnumerable<Event>, int)> GetAllByParamsAsync(EventQueryParams eventParams);
    Task<IEnumerable<Event>> GetAllUserEventsAsync (Guid userId);
}