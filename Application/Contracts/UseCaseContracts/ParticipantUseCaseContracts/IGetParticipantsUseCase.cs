using Application.DtoModels.CommonDto;
using Application.DtoModels.ParticipantDto;

namespace Application.Contracts.UseCaseContracts.ParticipantUseCaseContracts;

public interface IGetParticipantsUseCase
{
    Task<IEnumerable<GetParticipantDto>> Handle(PageParamsDto? pageParamsDto, Guid eventId);
}