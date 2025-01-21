using Domain.RepositoryContracts;

namespace Domain;

public interface IRepositoriesManager
{
    IEventRepository Events { get; }
    IParticipantRepository Participants { get; }
    ICategoryRepository Categories { get; }
    IUserRepository Users { get; }
    Task SaveAsync();
}