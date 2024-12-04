namespace BusinessLayer.DtoModels.ParticipantDto;

public class GetParticipantDto
{
    public Guid Id { get; set; } 
    
    public string Name { get; set; } = String.Empty;
    
    public string Surname { get; set; } = String.Empty;

    public string DateOfBirth { get; set; } = String.Empty;
  
    public string Email { get; set; } = String.Empty;
    public string RegistrationTime { get; set; } = String.Empty;
}