using DataLayer.Repositories.RepositoryContracts;
using DataLayer.Data;
using DataLayer.Models;
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
        await Repository.Events.FirstOrDefaultAsync(e => e.Id == id);

    public async Task<IEnumerable<Event>> GetAllAsync() =>
        await FindAll(trackChanges: false).Include(e => e.Category).ToListAsync();

    public async Task<IEnumerable<Event>> GetByDateAsync(DateTimeOffset date) => 
        await FindByCondition(e => e.DateTime == date, trackChanges: false)
            .ToListAsync();

    public async Task<IEnumerable<Event>> GetByDateRangeAsync(DateTimeOffset start, DateTimeOffset finish) =>
        await FindByCondition(e => e.DateTime > start && e.DateTime < finish, trackChanges: false)
            .ToListAsync();

    public async Task<IEnumerable<Event>> GetByCategoryAsync(Category category) =>
        await FindByCondition(e => e.Category == category, trackChanges: false)
            .ToListAsync();

    public async Task<IEnumerable<Event>> GetByPlaceAsync(string place) =>
        await FindByCondition(e => e.Place == place, trackChanges: false)
            .ToListAsync();
}