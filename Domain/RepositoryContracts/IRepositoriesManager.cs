using Domain.RepositoryContracts;

namespace Domain.RepositoryContracts;

public interface IRepositoriesManager
{
    IEventRepository Events { get; }
    IParticipantRepository Participants { get; }
    ICategoryRepository Categories { get; }
    IUserRepository Users { get; }
    Task SaveAsync();
}