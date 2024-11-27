using BusinessLayer.DtoModels.EventsDto;
using DataLayer.Models;

namespace BusinessLayer.Services.Contracts;

public interface IEventService
{
     Task<IEnumerable<GetEventDto>> GetAllAsync();
     Task<CreateEventDto> GetByIdAsync(Guid id);
     Task<CreateEventDto> GetByNameAsync(string name);
     Task<CreateEventDto> CreateAsync(CreateEventDto item);
     Task<CreateEventDto> UpdateAsync(Guid id, CreateEventDto item);
     Task<CreateEventDto> DeleteAsync(Guid id);
     Task<IEnumerable<GetEventDto>> GetByFiltersAsync(EventFiltersDto filtersDto);

}