using Contracts.RepositoryContracts;
using Entities;
using Entities.Models;

namespace Repository.Repositories;

public class ParticipantRepository : RepositoryBase<Participant>, IParticipantRepository
{
    public ParticipantRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }
    
    public void CreateParticipant(Participant participant) => Create(participant);

    public void DeleteParticipant(Participant participant) => Delete(participant);
}