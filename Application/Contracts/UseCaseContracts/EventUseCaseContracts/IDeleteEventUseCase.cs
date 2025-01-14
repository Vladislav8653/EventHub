using Application.DtoModels.EventsDto;

namespace Application.Contracts.UseCaseContracts.EventUseCaseContracts;

public interface IDeleteEventUseCase
{
    Task<GetEventDto> Handle(Guid id);
}