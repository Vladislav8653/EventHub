using Application.DtoModels.ParticipantDto;

namespace Application.Contracts.UseCaseContracts.ParticipantUseCaseContracts;

public interface IGetParticipantUseCase
{
    Task<GetParticipantDto> Handle(Guid eventId, Guid participantId);
}