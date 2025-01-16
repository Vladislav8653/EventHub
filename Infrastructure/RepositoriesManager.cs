using Application.Contracts;
using Application.Contracts.RepositoryContracts;

namespace Infrastructure;

public class RepositoriesManager : IRepositoriesManager
{
    private readonly EventHubDbContext _eventHubDbContext;
    private readonly IEventRepository _eventRepository;
    private readonly IParticipantRepository _participantRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUserRepository _userRepository;
    
    public RepositoriesManager(
        EventHubDbContext eventHubDbContext,
        IEventRepository eventRepository,
        IParticipantRepository participantRepository,
        ICategoryRepository categoryRepository,
        IUserRepository userRepository
        )
    {
        _eventHubDbContext = eventHubDbContext;
        _eventRepository = eventRepository;
        _participantRepository = participantRepository;
        _categoryRepository = categoryRepository;
        _userRepository = userRepository;
    }

    public IEventRepository Events => _eventRepository;
    public IParticipantRepository Participants => _participantRepository;
    public ICategoryRepository Categories => _categoryRepository;
    public IUserRepository Users => _userRepository;
    public async Task SaveAsync() => await _eventHubDbContext.SaveChangesAsync();
    
}