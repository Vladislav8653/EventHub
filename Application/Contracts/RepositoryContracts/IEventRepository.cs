using Application.DtoModels.CommonDto;
using Application.DtoModels.EventsDto;
using Domain.Models;

namespace Application.Contracts.RepositoryContracts;

public interface IEventRepository : IRepositoryBase<Event>
{
    Task<bool> IsUniqueNameAsync(string name);
    Task<Event?> GetByNameAsync(string name);
    Task<Event?> GetByIdAsync(Guid id);
    Task<PagedResult<Event>> GetAllByParamsAsync(EventQueryParams eventParams);
    Task<IEnumerable<Event>> GetAllUserEventsAsync (Guid userId);
}