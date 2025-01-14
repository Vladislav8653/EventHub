using Application.DtoModels.CommonDto;
using Application.DtoModels.EventsDto;

namespace Application.Contracts.UseCaseContracts.EventUseCaseContracts;

public interface IGetAllEventsUseCase
{
    Task<EntitiesWithTotalCountDto<GetEventDto>> Handle(EventQueryParamsDto eventParamsDto, HttpRequest request);

}