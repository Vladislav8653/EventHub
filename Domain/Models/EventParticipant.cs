namespace Domain.Models;

public class EventParticipant
{
    public Guid ParticipantId { get; set; }
    public Participant Participant { get; set; }
    public Guid EventId { get; set; }
    public Event Event { get; set; } 
    public DateTime RegistrationTime { get; set; }
}