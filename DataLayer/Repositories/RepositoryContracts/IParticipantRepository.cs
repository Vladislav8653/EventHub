using DataLayer.Models;

namespace DataLayer.Repositories.RepositoryContracts;

public interface IParticipantRepository : IRepositoryBase<Participant>
{
    Task<IEnumerable<Participant>> GetParticipantsAsync(Guid eventId);
    Task<Participant> RegisterParticipantAsync(Guid eventId, Participant participant);
    Task<Participant> GetParticipantAsync(Guid eventId, Guid participantId);
    Task<Participant> RemoveParticipantAsync(Guid eventId, Guid participantId);
}