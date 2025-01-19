using Application.DtoModels.EventsDto;
using Application.ImageService;

namespace Application.Contracts.UseCaseContracts.EventUseCaseContracts;

public interface IGetEventByNameUseCase
{
    Task<GetEventDto> Handle(string name, ImageUrlConfiguration request);
}