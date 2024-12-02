using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DataLayer.Models;

public class Category 
{
    [Column("CategoryId")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Category name is required")]
    [MaxLength(30, ErrorMessage = "Category Name can't be longer than 30")]
    public string Name { get; set; } = String.Empty;

    public ICollection<Event> Events { get; set; } = new List<Event>();

    public override bool Equals(object? obj)
    {
        if (obj is Category category)
        {
            return Name == category.Name; // сравниваю по значению строки 
        }
        return false;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode(); 
    }
    
}