using Contracts.RepositoryContracts;
using Entities;
using Entities.Models;

namespace Repository.Repositories;

public class EventParticipantRepository : 
    RepositoryBase<EventParticipant>, IEventParticipantRepository
{
    public EventParticipantRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }
    
    public void CreateEventParticipant(EventParticipant eventParticipant) => Create(eventParticipant);

    public void DeleteEventParticipant(EventParticipant eventParticipant) => Delete(eventParticipant);
}