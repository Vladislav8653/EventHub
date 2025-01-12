namespace Domain.Models;

public class Category 
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    
    /*
     * В прошлой версии здесь был перегружен метод Equals и GetHashCode для сравнения объектов этого типа.
     * Сейчас он убран, так категории просто сравниваются по имени в коде
     */
    
}