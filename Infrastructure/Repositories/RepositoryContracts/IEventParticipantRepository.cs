using Domain.Models;

namespace Infrastructure.Repositories.RepositoryContracts;

public interface IEventParticipantRepository : IRepositoryBase<EventParticipant>
{
    Task AttachParticipantToEvent(Participant participant, Event eventDb, DateTime regTime);
    Task DetachParticipantFromEvent(Guid eventId, Guid participantId);
}