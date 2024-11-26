namespace DataLayer.Models;

public class EventFilterDto
{
    public DateTime? Date { get; set; }

    public DateTime? StartDate { get; set; }
    public DateTime? FinishDate { get; set; }
    public string? Place { get; set; } 
    public Category? Category { get; set; } 
}