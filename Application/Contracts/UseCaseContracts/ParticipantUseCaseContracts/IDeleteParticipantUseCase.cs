using Application.DtoModels.ParticipantDto;

namespace Application.Contracts.UseCaseContracts.ParticipantUseCaseContracts;

public interface IDeleteParticipantUseCase
{
    Task<GetParticipantDto> Handle(Guid eventId, Guid participantId, Guid userId);
}