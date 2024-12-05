using DataLayer.Models;

namespace DataLayer.Specifications.Dto.Participants;

public class ParticipantWithAddInfoDto
{
    public required Participant Participant { get; set; }
    public required DateTime RegistrationTime { get; set; }
}