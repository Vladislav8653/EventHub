using BusinessLayer.DtoModels.ParticipantDto;

namespace BusinessLayer.Services.Contracts;

public interface IParticipantService
{
    Task<IEnumerable<GetParticipantDto>> GetParticipantsAsync(Guid eventId);
    Task<GetParticipantDto> RegisterParticipantAsync(Guid eventId, CreateParticipantDto participant);
    Task<GetParticipantDto> GetParticipantAsync(Guid eventId, Guid participantId);
    Task<GetParticipantDto> RemoveParticipantAsync(Guid eventId, Guid participantId);
}