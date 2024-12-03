using BusinessLayer.DtoModels.EventsDto;
using DataLayer.Models;
using Microsoft.AspNetCore.Http;

namespace BusinessLayer.Services.Contracts;

public interface IEventService
{
     Task<IEnumerable<GetEventDto>> GetAllAsync(HttpRequest request);
     Task<GetEventDto> GetByIdAsync(Guid id, HttpRequest request);
     Task<GetEventDto> GetByNameAsync(string name, HttpRequest request);
     Task<GetEventDto> CreateAsync(CreateEventDto item);
     Task<GetEventDto> UpdateAsync(Guid id, CreateEventDto item);
     Task<GetEventDto> DeleteAsync(Guid id);
     Task<IEnumerable<GetEventDto>> GetByFiltersAsync(EventFiltersDto filtersDto, HttpRequest request);
     Task<(byte[] fileBytes, string contentType)> GetImageAsync(string fileName);

}