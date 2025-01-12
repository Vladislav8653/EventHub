namespace DataLayer.Models;

public class Event
{
    public Guid Id { get; set; } 
    public string Name { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;
    public DateTime DateTime { get; set; }
    public string Place { get; set; } = String.Empty;
    public Guid CategoryId { get; set; }
    public Category Category { get; set; } 
    public uint MaxQuantityParticipant { get; set; }
    public ICollection<EventParticipant> Participants { get; set; } = new List<EventParticipant>();
    public string? Image { get; set; } = String.Empty;
}