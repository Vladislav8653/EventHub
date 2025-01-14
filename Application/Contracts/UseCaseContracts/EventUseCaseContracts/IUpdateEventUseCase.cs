using Application.DtoModels.EventsDto;

namespace Application.Contracts.UseCaseContracts.EventUseCaseContracts;

public interface IUpdateEventUseCase
{
    Task<GetEventDto> Handle(Guid id, CreateEventDto item);
}