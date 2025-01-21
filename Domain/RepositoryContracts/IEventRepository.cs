using Application.DtoModels.CommonDto;
using Domain.DTOs;
using Domain.Models;

namespace Domain.RepositoryContracts;

public interface IEventRepository : IRepositoryBase<Event>
{
    Task<bool> IsUniqueNameAsync(string name, CancellationToken cancellationToken);
    Task<Event?> GetByNameAsync(string name, CancellationToken cancellationToken);
    Task<Event?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<PagedResult<Event>> GetAllByParamsAsync(EventQueryParams eventParams, CancellationToken cancellationToken);
    Task<IEnumerable<Event>> GetAllUserEventsAsync (Guid userId, CancellationToken cancellationToken);
}