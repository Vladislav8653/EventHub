using BusinessLayer.DtoModels.CommonDto;
using BusinessLayer.DtoModels.ParticipantDto;

namespace BusinessLayer.Services.Contracts;

public interface IParticipantService
{
    Task<IEnumerable<GetParticipantDto>> GetParticipantsAsync(PageParamsDto? pageParams, Guid eventId);
    Task<RegistrationResult> RegisterParticipantAsync(Guid eventId, CreateParticipantDto participant, string userId);
    Task<GetParticipantDto> GetParticipantAsync(Guid eventId, Guid participantId);
    Task<GetParticipantDto> RemoveParticipantAsync(Guid eventId, Guid participantId,  string userId);
}