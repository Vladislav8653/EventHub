using Application.DtoModels.CommonDto;
using Application.DtoModels.EventsDto;

namespace Application.Contracts.UseCaseContracts;

public interface IEventService
{
     Task<EntitiesWithTotalCountDto<GetEventDto>> GetAllAsync(EventQueryParamsDto eventParamsDto, HttpRequest request);
     Task<GetEventDto> GetByIdAsync(Guid id, HttpRequest request);
     Task<GetEventDto> GetByNameAsync(string name, HttpRequest request);
     Task<GetEventDto> CreateAsync(CreateEventDto item);
     Task<GetEventDto> UpdateAsync(Guid id, CreateEventDto item);
     Task<GetEventDto> DeleteAsync(Guid id);
     Task<(byte[] fileBytes, string contentType)> GetImageAsync(string fileName);

}