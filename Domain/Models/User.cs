namespace Domain.Models;

public class User
{
    public Guid Id { get; set; }
    public string UserName { get; set; } = String.Empty;
    public string Login { get; set; } = String.Empty;
    public string Password { get; set; } = String.Empty;
    public string Role { get; set; } = String.Empty; // знаю, что так себе вариант, но пока так
    public ICollection<Participant> Participants = new List<Participant>(); // каждый юзер - это разный участник в разных событиях
}