namespace DataLayer.Models;

public class Category 
{
    public Guid Id { get; init; }
    public string Name { get; set; } = String.Empty;
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