using Application.Contracts.ImageServiceContracts;
using Application.DtoModels.EventsDto;

namespace Application.Contracts.UseCaseContracts.EventUseCaseContracts;

public interface IGetEventByIdUseCase
{
    Task<GetEventDto> Handle(Guid id, ImageUrlConfiguration request, CancellationToken cancellationToken);
}