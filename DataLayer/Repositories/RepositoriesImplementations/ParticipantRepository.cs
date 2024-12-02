using DataLayer.Repositories.RepositoryContracts;
using DataLayer.Data;
using DataLayer.Models;

namespace DataLayer.Repositories.RepositoriesImplementations;

public class ParticipantRepository : RepositoryBase<Participant>, IParticipantRepository
{
    public ParticipantRepository(EventHubDbContext eventHubDbContext) : base(eventHubDbContext) { }


    public async Task<IEnumerable<Participant>> GetParticipantsAsync(Guid eventId)
    {
        throw new NotImplementedException();
    }

    public async Task<Participant> RegisterParticipantAsync(Guid eventId, Participant participant)
    {
        throw new NotImplementedException();
    }

    public async Task<Participant> GetParticipantAsync(Guid eventId, Guid participantId)
    {
        throw new NotImplementedException();
    }

    public async Task<Participant> RemoveParticipantAsync(Guid eventId, Guid participantId)
    {
        throw new NotImplementedException();
    }
}