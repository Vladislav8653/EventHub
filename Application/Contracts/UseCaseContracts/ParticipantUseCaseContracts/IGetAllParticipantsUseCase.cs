using Application.DtoModels.CommonDto;
using Application.DtoModels.ParticipantDto;

namespace Application.Contracts.UseCaseContracts.ParticipantUseCaseContracts;

public interface IGetAllParticipantsUseCase
{
    Task<PagedResult<GetParticipantDto>> Handle(PageParamsDto? pageParamsDto, Guid eventId);
}