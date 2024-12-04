using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DataLayer.Models;

public class User
{
    [Column("UserId")]
    public Guid Id { get; set; } 

    [Required(ErrorMessage = "Login is required.")]
    [MaxLength(50, ErrorMessage = "Login can't be longer than 50 symbols")]
    public string Login { get; set; } = String.Empty;

    [Required(ErrorMessage = "Password is required.")]
    [MaxLength(50, ErrorMessage = "Password can't be longer than 50 symbols")]
    public string Password { get; set; } = String.Empty;
    
}