using Application.DtoModels.EventsDto;
using Application.Specifications;

namespace Application.Contracts.UseCaseContracts.EventUseCaseContracts;

public interface IGetEventByIdUseCase
{
    Task<GetEventDto> Handle(Guid id, ImageUrlConfiguration request);
}