namespace Application.DtoModels.ParticipantDto;

public class RegistrationResult
{
    public required string Message;
    public required bool Success;
    public required GetParticipantDto? Participant;
}