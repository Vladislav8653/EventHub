using Entities.Models;

namespace Contracts.RepositoryContracts;

public interface IEventRepository
{
    public Task<IEnumerable<Event>> GetAllEventsAsync(bool trackChanges);
    public Task<Event?> GetEventAsync(Guid eventId, bool trackChanges);
    public Task<Event?> GetEventAsync(string name, bool trackChanges);
    public void CreateEvent(Event @event);

    public void DeleteEvent(Event @event);
}