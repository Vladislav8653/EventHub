using Application.DtoModels.EventsDto;
using Application.ImageService;

namespace Application.Contracts.UseCaseContracts.EventUseCaseContracts;

public interface IGetEventByIdUseCase
{
    Task<GetEventDto> Handle(Guid id, ImageUrlConfiguration request);
}