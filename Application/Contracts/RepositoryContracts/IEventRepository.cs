using Application.DtoModels.CommonDto;
using Application.DtoModels.EventsDto;
using Domain.Models;

namespace Application.Contracts.RepositoryContracts;

public interface IEventRepository : IRepositoryBase<Event>
{
    Task<bool> IsUniqueNameAsync(string name, CancellationToken cancellationToken);
    Task<Event?> GetByNameAsync(string name, CancellationToken cancellationToken);
    Task<Event?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<PagedResult<Event>> GetAllByParamsAsync(EventQueryParams eventParams, CancellationToken cancellationToken);
    Task<IEnumerable<Event>> GetAllUserEventsAsync (Guid userId, CancellationToken cancellationToken);
}