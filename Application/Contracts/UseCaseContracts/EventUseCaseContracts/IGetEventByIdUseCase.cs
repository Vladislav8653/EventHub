using Application.DtoModels.EventsDto;

namespace Application.Contracts.UseCaseContracts.EventUseCaseContracts;

public interface IGetEventByIdUseCase
{
    Task<GetEventDto> Handle(Guid id, HttpRequest request);
}