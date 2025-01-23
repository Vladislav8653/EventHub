using Application.DtoModels.CommonDto;
using Application.DtoModels.ParticipantDto;
using Domain.DTOs;

namespace Application.Contracts.UseCaseContracts.ParticipantUseCaseContracts;

public interface IGetAllParticipantsUseCase
{
    Task<PagedResult<GetParticipantDto>> Handle(PageParamsDto? pageParamsDto, Guid eventId, CancellationToken cancellationToken);
}