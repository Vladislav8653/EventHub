namespace Domain.Models;

public class Event
{
    public Guid Id { get; init; } 
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public DateTime DateTime { get; init; }
    public string Place { get; init; } = string.Empty;
    public Guid CategoryId { get; set; }
    public Category Category { get; init; } = null!;
    public uint MaxQuantityParticipant { get; init; }
    public ICollection<Participant> Participants { get; init; } = new List<Participant>();
    public string? Image { get; set; } = string.Empty;
}