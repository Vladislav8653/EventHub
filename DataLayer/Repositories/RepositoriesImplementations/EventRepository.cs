using DataLayer.Repositories.RepositoryContracts;
using DataLayer.Data;
using DataLayer.Models;
using DataLayer.Models.Filters;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories.RepositoriesImplementations;

public class EventRepository : RepositoryBase<Event>, IEventRepository
{
    public EventRepository(EventHubDbContext eventHubDbContext) : base(eventHubDbContext) { }
    
    public async Task<bool> IsUniqueNameAsync(string name) => 
        !await Repository.Events.AnyAsync(e => e.Name == name);

    public async Task<Event?> GetByNameAsync(string name) =>
        await Repository.Events.FirstOrDefaultAsync(e => e.Name == name);

    public async Task<Event?> GetByIdAsync(Guid id) =>
        await Repository.Events.Include(e => e.Category).FirstOrDefaultAsync(e => e.Id == id);

    public async Task<IEnumerable<Event>> GetAllAsync() =>
        await FindAll(trackChanges: false).Include(e => e.Category).ToListAsync();
    

    public async Task<IEnumerable<Event>> GetByFiltersAsync(EventFilters filters)
    {
        var query = Repository.Events.Include(e => e.Category).AsQueryable();
        if (filters.Date.HasValue) // если событие в эту дату
        {
            query = query.Where(e => e.DateTime == filters.Date);
        }

        if (filters is { StartDate: not null, FinishDate: not null }) // если событие [c...по]
        {
            query = query.Where(e => e.DateTime > filters.StartDate && e.DateTime < filters.FinishDate);
        }

        if (filters.Category != null)
        {
            query = query.Where(e => e.Category == filters.Category);
        }

        if (filters.Place != null)
        {
            query = query.Where(e => e.Place == filters.Place);
        }

        return await query.ToListAsync();
    }
    
}