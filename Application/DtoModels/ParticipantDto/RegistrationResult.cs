namespace Application.DtoModels.ParticipantDto;

public class RegistrationResult
{
    public string Message { get; init; } = string.Empty;
    public bool Success  { get; init; }
    public GetParticipantDto? Participant { get; init; }
}