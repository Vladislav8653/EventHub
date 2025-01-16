using Domain.Models;

namespace Application.Specifications.Dto.Participants;

public class ParticipantWithAddInfoDto (Participant participant, DateTime regTime)
{
    public Participant Participant { get; } = participant;
    public DateTime RegistrationTime { get; } = regTime;
}