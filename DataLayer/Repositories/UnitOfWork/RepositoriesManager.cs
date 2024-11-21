using DataLayer.Data;
using DataLayer.Repositories.RepositoriesImplementations;
using DataLayer.Repositories.RepositoryContracts;


namespace DataLayer.Repositories.UnitOfWork;

public class RepositoriesManager : IRepositoriesManager
{
    private readonly EventHubDbContext _eventHubDbContext;
    private IEventRepository? _eventRepository;
    private IParticipantRepository? _participantRepository;
    private IEventParticipantRepository? _eventParticipantRepository;
    private ICategoryRepository? _categoryRepository;
    private IUserRepository? _userRepository;


    public RepositoriesManager(EventHubDbContext eventHubDbContext)
    {
        _eventHubDbContext = eventHubDbContext;
    }

    public IEventRepository Events
    {
        get
        {
            if (_eventRepository == null)
                _eventRepository = new EventRepository(_eventHubDbContext);
            return _eventRepository;
        }
    }

    public IParticipantRepository Participants
    {
        get
        {
            if (_participantRepository == null)
                _participantRepository = new ParticipantRepository(_eventHubDbContext);
            return _participantRepository;
        }
    }

    public IEventParticipantRepository EventsParticipants
    {
        get
        {
            if (_eventParticipantRepository == null)
                _eventParticipantRepository = new EventParticipantRepository(_eventHubDbContext);
            return _eventParticipantRepository;
        }
    }

    public ICategoryRepository Categories
    {
        get
        {
            if (_categoryRepository == null)
                _categoryRepository = new CategoryRepository(_eventHubDbContext);
            return _categoryRepository;
        }
    }

    public IUserRepository Users
    {
        get
        {
            if (_userRepository == null)
                _userRepository = new UserRepository(_eventHubDbContext);
            return _userRepository;
        }
    }

    public async Task SaveAsync() => await _eventHubDbContext.SaveChangesAsync();
    
}