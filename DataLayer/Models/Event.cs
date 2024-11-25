using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models;

public class Event
{
    [Column("EventId")]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Required(ErrorMessage = "Event name is required.")]
    [MaxLength(100, ErrorMessage = "Event name can't be longer than 100 symbols")]
    public string Name { get; set; } = String.Empty;
    
    [MaxLength(1000, ErrorMessage = "Description can't be longer than 1000 symbols")]
    public string Description { get; set; } = String.Empty;
    
    [Required(ErrorMessage = "Date and time is required.")]
    //public DateTimeOffset DateTime { get; set; }
    public DateTime DateTime { get; set; }

    [Required(ErrorMessage = "Place is required.")]
    [MaxLength(100, ErrorMessage = "Place can't be longer than 100 symbols")]
    public string Place { get; set; } = String.Empty;
    
    public Guid CategoryId { get; set; }
    public Category Category { get; set; } 

    public uint MaxQuantityParticipant { get; set; }

    public ICollection<EventParticipant> Participants { get; set; } = new List<EventParticipant>();

    [MaxLength(100, ErrorMessage = "Image link can't be longer than 100 symbols")]
    public string Image { get; set; } = String.Empty;
}