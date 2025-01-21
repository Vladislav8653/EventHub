using Application.Contracts.ImageServiceContracts;
using Application.DtoModels.EventsDto;

namespace Application.Contracts.UseCaseContracts.EventUseCaseContracts;

public interface IGetAllUserEventsUseCase
{
    Task<IEnumerable<GetEventDto>> Handle(Guid userId, ImageUrlConfiguration request, CancellationToken cancellationToken);
}