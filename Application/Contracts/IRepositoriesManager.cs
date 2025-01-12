using Application.Contracts.RepositoryContracts;

namespace Application.Contracts;

public interface IRepositoriesManager
{
    IEventRepository Events { get; }
    IParticipantRepository Participants { get; }
    IEventParticipantRepository EventsParticipants { get; }
    ICategoryRepository Categories { get; }
    IUserRepository Users { get; }
    Task SaveAsync();
}