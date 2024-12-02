using BusinessLayer.DtoModels.ParticipantDto;

namespace BusinessLayer.Services.Contracts;

public interface IParticipantService
{
    Task<IEnumerable<GetParticipantDto>> GetParticipantsAsync(Guid eventId);
    Task<CreateParticipantDto> RegisterParticipantAsync(Guid eventId, CreateParticipantDto participant);
    Task<CreateParticipantDto> GetParticipantAsync(Guid eventId, Guid participantId);
    Task<CreateParticipantDto> RemoveParticipantAsync(Guid eventId, Guid participantId);
}