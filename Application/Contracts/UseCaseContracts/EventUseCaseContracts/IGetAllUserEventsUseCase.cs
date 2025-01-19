using Application.DtoModels.EventsDto;
using Application.ImageService;

namespace Application.Contracts.UseCaseContracts.EventUseCaseContracts;

public interface IGetAllUserEventsUseCase
{
    Task<IEnumerable<GetEventDto>> Handle(Guid userId, ImageUrlConfiguration request);
}