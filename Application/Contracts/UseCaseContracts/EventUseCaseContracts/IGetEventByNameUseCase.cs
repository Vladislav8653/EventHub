using Application.DtoModels.EventsDto;
using Application.Specifications;

namespace Application.Contracts.UseCaseContracts.EventUseCaseContracts;

public interface IGetEventByNameUseCase
{
    Task<GetEventDto> Handle(string name, ImageUrlConfiguration request);
}