namespace DataLayer.Models;

public class Participant
{
    public Guid Id { get; set; } 
    public string Name { get; set; } = String.Empty;
    public string Surname { get; set; } = String.Empty;
    public DateOnly DateOfBirth { get; set; }
    public string Email { get; set; } = String.Empty;
    public ICollection<EventParticipant> Events { get; set; } = new List<EventParticipant>();
    public User User { get; set; }
    public Guid UserId { get; set; }
}
