namespace Application.DtoModels.ParticipantDto;

public class RegistrationResult
{
    public required string Message { get; set; }
    public required bool Success { get; set; }
    public required GetParticipantDto? Participant { get; set; } 
}