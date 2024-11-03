using Contracts.RepositoryContracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repositories;

public class EventRepository : 
    RepositoryBase<Event>, IEventRepository
{
    public EventRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }


    public async Task<IEnumerable<Event>> GetAllEventsAsync(bool trackChanges) =>
         await FindAll(trackChanges)
        .OrderBy(e => e.DateTime)
        .ToListAsync();


    public async Task<Event?> GetEventAsync(Guid eventId, bool trackChanges) =>
        await FindByCondition(e => e.Id == eventId, trackChanges)
            .SingleOrDefaultAsync();

    public async Task<Event?> GetEventAsync(string name, bool trackChanges) =>
        await FindByCondition(e => e.Name == name, trackChanges)
            .OrderBy(e => e.DateTime)
            .SingleOrDefaultAsync();

    public void CreateEvent(Event @event) => Create(@event);

    public void DeleteEvent(Event @event) => Delete(@event);
}