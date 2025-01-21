using System.Linq.Expressions;
using Domain.RepositoryContracts;
using Application.DtoModels.CommonDto;
using Domain.DTOs;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using LinqKit;

namespace Infrastructure.RepositoryImplementations;

public class EventRepository : RepositoryBase<Event>, IEventRepository
{
    public EventRepository(EventHubDbContext eventHubDbContext) : base(eventHubDbContext) { }
    
    public async Task<bool> IsUniqueNameAsync(string name, CancellationToken cancellationToken) => 
        !await Repository.Events.AnyAsync(e => e.Name == name, cancellationToken);

    public async Task<Event?> GetByNameAsync(string name, CancellationToken cancellationToken) =>
        await Repository.Events.FirstOrDefaultAsync(e => e.Name == name, cancellationToken);

    public async Task<Event?> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
        await Repository.Events
            .Include(e => e.Category)
            .Include(e => e.Participants)
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    
    public async Task<IEnumerable<Event>> GetAllUserEventsAsync(Guid userId, CancellationToken cancellationToken) =>
        await Repository.Participants
            .Where(p => p.UserId == userId)
            .Include(p => p.Event)
            .Select(p => p.Event)
            .ToListAsync(cancellationToken);
    
    public async Task<PagedResult<Event>> GetAllByParamsAsync(EventQueryParams eventParams, CancellationToken cancellationToken)
    {
        var query = Repository.Events.Include(e => e.Category).AsQueryable();
        var filters = eventParams.Filters ?? null;
        if (filters == null) 
            return await GetByPageAsync(query, eventParams.PageParams, cancellationToken);
        Expression<Func<Event, bool>> filter = e => true;
        if (filters.Date.HasValue) // если событие в эту дату
            filter = filter.And(e => e.DateTime == filters.Date);
        if (filters is { StartDate: not null, FinishDate: not null }) // если событие [с...по]
            filter = filter.And(e => e.DateTime > filters.StartDate && e.DateTime < filters.FinishDate);
        if (filters.Category != null)
            filter = filter.And(e => e.Category == filters.Category);
        if (!string.IsNullOrEmpty(filters.Place))
            filter = filter.And(e => e.Place == filters.Place);
        return await GetByPageAsync(query.Where(filter), eventParams.PageParams, cancellationToken);
    }
}