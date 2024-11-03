using DataLayer.Repositories.RepositoryContracts;

namespace DataLayer.Repositories.UnitOfWork;

public interface IRepositoryManager
{
    IEventRepository EventRepository { get; }
    IParticipantRepository ParticipantRepository { get; }
    IEventParticipantRepository EventParticipantRepository { get; }
    ICategoryRepository CategoryRepository { get; }
    IUserRepository UserRepository { get; }
    Task SaveAsync();
}