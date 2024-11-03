using DataLayer.Data;
using DataLayer.Repositories.RepositoriesImplementations;
using DataLayer.Repositories.RepositoryContracts;


namespace DataLayer.Repositories.UnitOfWork;

public class RepositoriesesManager : IRepositoriesManager
{
    private readonly EventHubDbContext _eventHubDbContext;
    private IEventRepository? _eventRepository;
    private IParticipantRepository? _participantRepository;
    private IEventParticipantRepository? _eventParticipantRepository;
    private ICategoryRepository? _categoryRepository;
    private IUserRepository? _userRepository;


    public RepositoriesesManager(EventHubDbContext eventHubDbContext)
    {
        _eventHubDbContext = eventHubDbContext;
    }

    public IEventRepository EventRepository
    {
        get
        {
            if (_eventRepository == null)
                _eventRepository = new EventRepository(_eventHubDbContext);
            return _eventRepository;
        }
    }

    public IParticipantRepository ParticipantRepository
    {
        get
        {
            if (_participantRepository == null)
                _participantRepository = new ParticipantRepository(_eventHubDbContext);
            return _participantRepository;
        }
    }

    public IEventParticipantRepository EventParticipantRepository
    {
        get
        {
            if (_eventParticipantRepository == null)
                _eventParticipantRepository = new EventParticipantRepository(_eventHubDbContext);
            return _eventParticipantRepository;
        }
    }

    public ICategoryRepository CategoryRepository
    {
        get
        {
            if (_categoryRepository == null)
                _categoryRepository = new CategoryRepository(_eventHubDbContext);
            return _categoryRepository;
        }
    }

    public IUserRepository UserRepository
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