using DataLayer.Models;

namespace DataLayer.Repositories.RepositoryContracts;

public interface IParticipantRepository : IRepositoryBase<Participant>
{
    Task<IEnumerable<Participant>> GetParticipantsAsync(Guid eventId);
    Task RegisterParticipantAsync(Event eventDb, Participant participant, DateTime registrationTime);
    Task<Participant?> GetParticipantAsync(Guid participantId);
    Task RemoveParticipantAsync(Guid eventId, Participant participant);
}