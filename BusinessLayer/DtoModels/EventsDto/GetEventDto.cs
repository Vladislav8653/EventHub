namespace BusinessLayer.DtoModels.EventsDto;

public class GetEventDto
{
    public Guid Id  { get; set; }
    public string Name { get; set; } = String.Empty;
    
    public string Description { get; set; } = String.Empty;

    public string DateTime { get; set; } = String.Empty; // мб я не прав

    public string Place { get; set; } = String.Empty;

    public string Category { get; set; } = String.Empty;

    public uint MaxQuantityParticipant { get; set; }
    public string? ImageUrl { get; set; } 
}