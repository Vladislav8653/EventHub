using BusinessLayer.DtoModels.EventsDto;
using DataLayer.Models;

namespace BusinessLayer.Services.Contracts;

public interface IEventService
{
     Task<IEnumerable<Event>> GetAllAsync();
     Task<IEnumerable<Event>> GetByDateAsync(string date);
     Task<IEnumerable<Event>> GetByDateRangeAsync(string start, string finish);
     Task<IEnumerable<Event>> GetByPlaceAsync(string place);
     Task<IEnumerable<Event>> GetByCategoryAsync(string categoryText);
     Task<Event> GetByIdAsync(Guid id);
     Task<Event> GetByNameAsync(string name);
     Task<Event> CreateAsync(CreateEventDto item);
     Task<Event> UpdateAsync(Guid id, UpdateEventDto item);
     Task<Event> DeleteAsync(Guid id);
    
}