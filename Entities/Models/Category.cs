using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class Category
{
    [Column("CategoryId")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Category name is required")]
    [MaxLength(30, ErrorMessage = "Category Name can't be longer than 30")]
    public string CategoryName { get; set; } = String.Empty;

    public ICollection<Event> Events { get; set; } = [];

}