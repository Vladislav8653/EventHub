using BusinessLayer.DtoModels.EventsDto;
using DataLayer.Models;

namespace BusinessLayer.Services.Contracts;

public interface IEventService
{
     Task<IEnumerable<GetEventDto>> GetAllAsync();
     Task<IEnumerable<Event>> GetByDateAsync(string date);
     Task<IEnumerable<Event>> GetByDateRangeAsync(string start, string finish);
     Task<IEnumerable<Event>> GetByPlaceAsync(string place);
     Task<IEnumerable<Event>> GetByCategoryAsync(string categoryText);
     Task<CreateEventDto> GetByIdAsync(Guid id);
     Task<CreateEventDto> GetByNameAsync(string name);
     Task<CreateEventDto> CreateAsync(CreateEventDto item);
     Task<CreateEventDto> UpdateAsync(Guid id, CreateEventDto item);
     Task<CreateEventDto> DeleteAsync(Guid id);
    
}