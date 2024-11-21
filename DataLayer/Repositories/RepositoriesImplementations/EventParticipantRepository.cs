using DataLayer.Repositories.RepositoryContracts;
using DataLayer.Data;
using DataLayer.Models;

namespace DataLayer.Repositories.RepositoriesImplementations;

public class EventParticipantRepository : 
    RepositoryBase<EventParticipant>, IEventParticipantRepository
{
    public EventParticipantRepository(EventHubDbContext eventHubDbContext) : base(eventHubDbContext) { }
    
    /*public void CreateEventParticipant(EventParticipant eventParticipant) => Create(eventParticipant);

    public void DeleteEventParticipant(EventParticipant eventParticipant) => Delete(eventParticipant);*/
}