using DataLayer.Data;
using DataLayer.Repositories.RepositoryContracts;


namespace DataLayer.Repositories.UnitOfWork;

public class RepositoriesManager : IRepositoriesManager
{
    private readonly EventHubDbContext _eventHubDbContext;
    private readonly IEventRepository _eventRepository;
    private readonly IParticipantRepository _participantRepository;
    private readonly IEventParticipantRepository _eventParticipantRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUserRepository _userRepository;
    
    public RepositoriesManager(
        EventHubDbContext eventHubDbContext,
        IEventRepository eventRepository,
        IParticipantRepository participantRepository,
        IEventParticipantRepository eventParticipantRepository,
        ICategoryRepository categoryRepository,
        IUserRepository userRepository
        )
    {
        _eventHubDbContext = eventHubDbContext;
        _eventRepository = eventRepository;
        _participantRepository = participantRepository;
        _eventParticipantRepository = eventParticipantRepository;
        _categoryRepository = categoryRepository;
        _userRepository = userRepository;
    }

    public IEventRepository Events => _eventRepository;
    public IEventParticipantRepository EventsParticipants => _eventParticipantRepository;
    public IParticipantRepository Participants => _participantRepository;
    public ICategoryRepository Categories => _categoryRepository;
    public IUserRepository Users => _userRepository;
    public async Task SaveAsync() => await _eventHubDbContext.SaveChangesAsync();
    
}