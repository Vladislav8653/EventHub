using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models;

public class Participant
{
    [Column("ParticipantId")]
    public Guid Id { get; set; } 
    
    [Required(ErrorMessage = "Name is required")]
    [MaxLength(30, ErrorMessage = "Name can't be longer than 30")]
    public string Name { get; set; } = String.Empty;
    
    [Required(ErrorMessage = "Surname is required")]
    [MaxLength(30, ErrorMessage = "Surname can't be longer than 30")]
    public string Surname { get; set; } = String.Empty;
    
    [Required(ErrorMessage = "Date of birth is required")]
    public DateOnly DateOfBirth { get; set; }
    
    [Required(ErrorMessage = "Email is required")]
    [MaxLength(254, ErrorMessage = "Email can't be longer than 254")]
    public string Email { get; set; } = String.Empty;

    public ICollection<EventParticipant> Events { get; set; } = new List<EventParticipant>();
}