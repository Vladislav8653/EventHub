using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models;

public class EventParticipant
{
    [Required]
    public Guid ParticipantId { get; set; }
    [Required]
    public Participant? Participant { get; set; }
    
    [Required]
    public Guid EventId { get; set; }
    [Required]
    public Event? Event { get; set; } 
    
    [Required]
    public DateTimeOffset RegistrationTime { get; set; }
}