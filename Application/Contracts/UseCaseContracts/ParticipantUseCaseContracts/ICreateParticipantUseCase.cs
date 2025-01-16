using Application.DtoModels.ParticipantDto;

namespace Application.Contracts.UseCaseContracts.ParticipantUseCaseContracts;

public interface ICreateParticipantUseCase
{
    Task<RegistrationResult> Handle(Guid eventId, CreateParticipantDto item, string userIdStr);
}