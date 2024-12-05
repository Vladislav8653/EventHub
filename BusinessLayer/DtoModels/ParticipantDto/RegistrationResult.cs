namespace BusinessLayer.DtoModels.ParticipantDto;

public class RegistrationResult
{
    /*public RegistrationResult(bool success, string message, GetParticipantDto? participant)
    {
        Success = success;
        Message = message;
        Participant = participant;
    }*/

    public RegistrationResult() { }
    public required string Message { get; set; }
    public required bool Success { get; set; }
    public required GetParticipantDto? Participant { get; set; } 
}