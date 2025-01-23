using Application.Contracts.ImageServiceContracts;
using Application.DtoModels.CommonDto;
using Application.DtoModels.EventsDto;
using Domain.DTOs;

namespace Application.Contracts.UseCaseContracts.EventUseCaseContracts;

public interface IGetAllEventsUseCase
{
    Task<PagedResult<GetEventDto>> Handle(EventQueryParamsDto eventParamsDto, ImageUrlConfiguration request, CancellationToken cancellationToken);
}