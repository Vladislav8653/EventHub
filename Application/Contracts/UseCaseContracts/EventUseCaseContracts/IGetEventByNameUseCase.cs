using Application.DtoModels.EventsDto;

namespace Application.Contracts.UseCaseContracts.EventUseCaseContracts;

public interface IGetEventByNameUseCase
{
    Task<GetEventDto> Handle(string name, HttpRequest request);
}