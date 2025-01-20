using Application.DtoModels.EventsDto;

namespace Application.Contracts.UseCaseContracts.EventUseCaseContracts;

public interface ICreateEventUseCase
{
    Task<GetEventDto> Handle(CreateEventDto item, string imageStoragePath, CancellationToken cancellationToken);
}