using DataLayer.Repositories.RepositoryContracts;
using DataLayer.Data;
using DataLayer.Models;

namespace DataLayer.Repositories.RepositoriesImplementations;

public class ParticipantRepository : RepositoryBase<Participant>, IParticipantRepository
{
    public ParticipantRepository(EventHubDbContext eventHubDbContext) : base(eventHubDbContext) { }
    
    /*public void CreateParticipant(Participant participant) => Create(participant);

    public void DeleteParticipant(Participant participant) => Delete(participant);*/
}