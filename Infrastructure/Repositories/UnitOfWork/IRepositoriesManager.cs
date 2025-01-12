using Infrastructure.Repositories.RepositoryContracts;

namespace Infrastructure.Repositories.UnitOfWork;

public interface IRepositoriesManager
{
    IEventRepository Events { get; }
    IParticipantRepository Participants { get; }
    IEventParticipantRepository EventsParticipants { get; }
    ICategoryRepository Categories { get; }
    IUserRepository Users { get; }
    Task SaveAsync();
}