namespace Domain.Models;

public class User 
{
    public Guid Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string? RefreshToken { get; set; } = string.Empty;
    public DateTime? RefreshTokenExpireTime { get; set; }
    public ICollection<Participant> Participants = new List<Participant>(); // каждый юзер - это разный участник в разных событиях
}