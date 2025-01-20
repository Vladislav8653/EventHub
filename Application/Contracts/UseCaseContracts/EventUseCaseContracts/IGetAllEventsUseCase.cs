using Application.DtoModels.CommonDto;
using Application.DtoModels.EventsDto;
using Application.ImageService;

namespace Application.Contracts.UseCaseContracts.EventUseCaseContracts;

public interface IGetAllEventsUseCase
{
    Task<PagedResult<GetEventDto>> Handle(EventQueryParamsDto eventParamsDto, ImageUrlConfiguration request, CancellationToken cancellationToken);
}