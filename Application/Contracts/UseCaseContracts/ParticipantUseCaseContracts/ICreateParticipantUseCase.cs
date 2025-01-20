using Application.DtoModels.ParticipantDto;

namespace Application.Contracts.UseCaseContracts.ParticipantUseCaseContracts;

public interface ICreateParticipantUseCase
{
    Task<RegistrationResult> Handle(Guid eventId, CreateParticipantDto item, Guid userId, CancellationToken cancellationToken);
}