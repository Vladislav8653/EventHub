﻿namespace BusinessLayer.DtoModels.EventsDto;

public class UpdateEventDto
{
    public string Name { get; set; } = String.Empty;
    
    public string Description { get; set; } = String.Empty;

    public string DateTime { get; set; } = String.Empty;

    public string Place { get; set; } = String.Empty;

    public string Category { get; set; } = String.Empty;

    public uint MaxQuantityParticipant { get; set; }

    public string Image { get; set; } = String.Empty;
}