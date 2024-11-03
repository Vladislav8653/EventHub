namespace BusinessLayer.DtoModels.EventsDto;

public class CreateEventDto
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = String.Empty;
    
    public string Description { get; set; } = String.Empty;

    public DateTimeOffset DateTime { get; set; }

    public string Place { get; set; } = String.Empty;

    public string Category { get; set; } = String.Empty;

    public uint MaxQuantityParticipant { get; set; }

    public string Image { get; set; } = String.Empty;
}