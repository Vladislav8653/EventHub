using Entities.Models;

namespace Contracts.RepositoryContracts;

public interface IParticipantRepository
{
    public void CreateParticipant(Participant participant);

    public void DeleteParticipant(Participant participant);
}