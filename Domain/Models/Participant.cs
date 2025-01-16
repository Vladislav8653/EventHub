namespace Domain.Models;

public class Participant
{
    public Guid Id { get; init; } 
    public string Name { get; init; } = String.Empty;
    public string Surname { get; init; } = String.Empty;
    public DateOnly DateOfBirth { get; init; }
    public string Email { get; init; } = String.Empty;
    
    public DateTime RegistrationTime { get; set; }
    public Event Event { get; set; } 
    public Guid EventId { get; set; }
    public User User { get; set; }
    public Guid UserId { get; set; }
}
