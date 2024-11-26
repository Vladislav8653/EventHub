using AutoMapper;
using BusinessLayer.DtoModels.EventsDto;
using BusinessLayer.Exceptions;
using BusinessLayer.Services.Contracts;
using DataLayer.Models;
using DataLayer.Repositories.UnitOfWork;

namespace BusinessLayer.Services.Implementations;

public class EventService : IEventService
{
    private readonly IRepositoriesManager _repositoriesManager;
    private readonly IMapper _mapper;
    public EventService(IRepositoriesManager repositoriesManager, IMapper mapper)
    {
        _repositoriesManager = repositoriesManager;
        _mapper = mapper;
    }


    public async Task<IEnumerable<GetEventDto>> GetAllAsync()
    {
        var events = await _repositoriesManager.Events.GetAllAsync();
        var eventsDto = _mapper.Map<IEnumerable<GetEventDto>>(events);
        return eventsDto;
    }
    
    public async Task<CreateEventDto> GetByIdAsync(Guid id)
    {
        var eventById = await _repositoriesManager.Events.GetByIdAsync(id);
        if (eventById == null)
            throw new EntityNotFoundException($"Event with id {id} doesn't exist");
        var eventDto = _mapper.Map<CreateEventDto>(eventById);
        return eventDto;
    }

    public async Task<CreateEventDto> GetByNameAsync(string name)
    {
        var eventByName = await _repositoriesManager.Events.GetByNameAsync(name);
        if (eventByName == null)
            throw new EntityNotFoundException($"Event with name {name} doesn't exist");
        var eventDto = _mapper.Map<CreateEventDto>(eventByName);
        return eventDto;
    }

    public async Task<CreateEventDto> CreateAsync(CreateEventDto item)
    {
        var isUniqueName = await _repositoriesManager.Events.IsUniqueNameAsync(item.Name);
        if (!isUniqueName)
            throw new EntityAlreadyExistException(nameof(Event), "name", item.Name);
        var category = await _repositoriesManager.Categories.TryGetByNameAsync(item.Category);
        if (category == null)
            throw new EntityNotFoundException($"Category {item.Category} doesn't exits.");
        var eventForDb = _mapper.Map<Event>(item);
        eventForDb.CategoryId = category.Id;
        //eventForDb.Category = category;
        await _repositoriesManager.Events.CreateAsync(eventForDb);
        await _repositoriesManager.SaveAsync();
        var eventDto = _mapper.Map<CreateEventDto>(eventForDb);
        return eventDto;
    }

    public async Task<CreateEventDto> UpdateAsync(Guid id, CreateEventDto item) // TODO!!!!!!
    {
        var eventToUpdate = await _repositoriesManager.Events.GetByIdAsync(id);
        if (eventToUpdate == null)
            throw new EntityNotFoundException($"Event with id {id} doesn't exist");
        _mapper.Map(item, eventToUpdate);
        _repositoriesManager.Events.Update(eventToUpdate);
        await _repositoriesManager.SaveAsync();
        var eventDto = _mapper.Map<CreateEventDto>(eventToUpdate);
        return eventDto;
    }

    public async Task<CreateEventDto> DeleteAsync(Guid id)
    {
        var eventToDelete = await _repositoriesManager.Events.GetByIdAsync(id);
        if (eventToDelete == null)
            throw new EntityNotFoundException($"Event with id {id} doesn't exist");
        _repositoriesManager.Events.Delete(eventToDelete);
        await _repositoriesManager.SaveAsync();
        var eventDto = _mapper.Map<CreateEventDto>(eventToDelete);
        return eventDto;
    }
}