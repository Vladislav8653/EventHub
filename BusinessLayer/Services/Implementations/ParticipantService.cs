using BusinessLayer.DtoModels.ParticipantDto;
using BusinessLayer.Services.Contracts;

namespace BusinessLayer.Services.Implementations;

public class ParticipantService : IParticipantService
{
    public async Task<IEnumerable<GetParticipantDto>> GetParticipantsAsync(Guid eventId)
    {
        throw new NotImplementedException();
    }

    public async Task<CreateParticipantDto> RegisterParticipantAsync(Guid eventId, CreateParticipantDto participant)
    {
        throw new NotImplementedException();
    }

    public async Task<CreateParticipantDto> GetParticipantAsync(Guid eventId, Guid participantId)
    {
        throw new NotImplementedException();
    }

    public async Task<CreateParticipantDto> RemoveParticipantAsync(Guid eventId, Guid participantId)
    {
        throw new NotImplementedException();
    }
}