using Entities.Models;

namespace Contracts.RepositoryContracts;

public interface IEventParticipantRepository
{
    public void CreateEventParticipant(EventParticipant eventParticipant);

    public void DeleteEventParticipant(EventParticipant eventParticipant);
}