namespace DataLayer.Models.Dto;

public class ParticipantWithAddInfoDto
{
    public required Participant Participant { get; set; }
    public required DateTime RegistrationTime { get; set; }
}