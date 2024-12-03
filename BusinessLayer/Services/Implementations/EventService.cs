using AutoMapper;
using BusinessLayer.DtoModels.EventsDto;
using BusinessLayer.Exceptions;
using BusinessLayer.Services.Contracts;
using DataLayer.Models;
using DataLayer.Models.Filters;
using DataLayer.Repositories.UnitOfWork;
using Microsoft.AspNetCore.Http;


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


    public async Task<IEnumerable<GetEventDto>> GetAllAsync(HttpRequest request)
    {
        var events = await _repositoriesManager.Events.GetAllAsync();
        foreach (var eventDb in events)
        {
            if (!string.IsNullOrEmpty(eventDb.Image))
                eventDb.Image = new Uri(new Uri($"{request.Scheme}://{request.Host}"), $"events/images/{eventDb.Image}").ToString();
        }
        var eventsDto = _mapper.Map<IEnumerable<GetEventDto>>(events);
        return eventsDto;
    }
    
    public async Task<GetEventDto> GetByIdAsync(Guid id, HttpRequest request)
    {
        
        var eventById = await _repositoriesManager.Events.GetByIdAsync(id);
        if (eventById == null)
            throw new EntityNotFoundException($"Event with id {id} doesn't exist");
        if (!string.IsNullOrEmpty(eventById.Image))
            eventById.Image = new Uri(new Uri($"{request.Scheme}://{request.Host}"), $"events/images/{eventById.Image}").ToString();
        var eventDto = _mapper.Map<GetEventDto>(eventById);
        return eventDto;
    }

    public async Task<GetEventDto> GetByNameAsync(string name, HttpRequest request)
    {
        var eventByName = await _repositoriesManager.Events.GetByNameAsync(name);
        if (eventByName == null)
            throw new EntityNotFoundException($"Event with name {name} doesn't exist");
        if (!string.IsNullOrEmpty(eventByName.Image))
            eventByName.Image = new Uri(new Uri($"{request.Scheme}://{request.Host}"), $"events/images/{eventByName.Image}").ToString();

        var eventDto = _mapper.Map<GetEventDto>(eventByName);
        return eventDto;
    }

    public async Task<IEnumerable<GetEventDto>> GetByFiltersAsync(EventFiltersDto filtersDto, HttpRequest request)
    {
        var filters = _mapper.Map<EventFilters>(filtersDto);
        if (filtersDto.Category != null)
        {
            var category = await _repositoriesManager.Categories.TryGetByNameAsync(filtersDto.Category);
            if (category != null)
            {
                filters.Category = category;
            }
        }
        var eventsByFilters = await _repositoriesManager.Events.GetByFiltersAsync(filters);
        foreach (var eventDb in eventsByFilters)
        {
            if (!string.IsNullOrEmpty(eventDb.Image))
                eventDb.Image = new Uri(new Uri($"{request.Scheme}://{request.Host}"), $"events/images/{eventDb.Image}").ToString();
        }
        var eventsByFiltersDto = _mapper.Map<IEnumerable<GetEventDto>>(eventsByFilters);
        return eventsByFiltersDto;
    }

    public async Task<GetEventDto> CreateAsync(CreateEventDto item)
    {
        var isUniqueName = await _repositoriesManager.Events.IsUniqueNameAsync(item.Name);
        if (!isUniqueName)
            throw new EntityAlreadyExistException(nameof(Event), "name", item.Name);
        var category = await _repositoriesManager.Categories.TryGetByNameAsync(item.Category);
        if (category == null)
            throw new EntityNotFoundException($"Category {item.Category} doesn't exits.");
        var eventForDb = _mapper.Map<Event>(item);
        
        string? filename = null;
        if (item.Image != null) 
        {
            var imageFilePath = Path.Combine("wwwroot", "images", item.Image.FileName);
            filename = item.Image.FileName;
            await WriteFileAsync(item.Image, imageFilePath);
        }
        eventForDb.Image = filename;
        eventForDb.CategoryId = category.Id;
        
        await _repositoriesManager.Events.CreateAsync(eventForDb);
        await _repositoriesManager.SaveAsync();
        var eventDto = _mapper.Map<GetEventDto>(eventForDb);
        return eventDto;
    }

    public async Task<GetEventDto> UpdateAsync(Guid id, CreateEventDto item) // TODO!!!!!!
    {
        var eventToUpdate = await _repositoriesManager.Events.GetByIdAsync(id);
        if (eventToUpdate == null)
            throw new EntityNotFoundException($"Event with id {id} doesn't exist");
        var category = await _repositoriesManager.Categories.TryGetByNameAsync(item.Category);
        if (category == null)
            throw new EntityNotFoundException($"Category {item.Category} doesn't exits.");
        _mapper.Map(item, eventToUpdate);
        
        string? filename = null;
        if (item.Image != null) 
        {
            var imageFilePath = Path.Combine("wwwroot", "images", item.Image.FileName);
            filename = item.Image.FileName;
            await WriteFileAsync(item.Image, imageFilePath);
        }
        eventToUpdate.Image = filename;
        eventToUpdate.CategoryId = category.Id;
        
        _repositoriesManager.Events.Update(eventToUpdate);
        await _repositoriesManager.SaveAsync();
        var eventDto = _mapper.Map<GetEventDto>(eventToUpdate);
        return eventDto;
    }

    public async Task<GetEventDto> DeleteAsync(Guid id)
    {
        var eventToDelete = await _repositoriesManager.Events.GetByIdAsync(id);
        if (eventToDelete == null)
            throw new EntityNotFoundException($"Event with id {id} doesn't exist");
        _repositoriesManager.Events.Delete(eventToDelete);
        await _repositoriesManager.SaveAsync();
        var eventDto = _mapper.Map<GetEventDto>(eventToDelete);
        return eventDto;
    }

    public async Task<(byte[] fileBytes, string contentType)> GetImageAsync(string fileName)
    {
        var filePath = Path.Combine("wwwroot", "images", fileName);
        if (!File.Exists(filePath))
        {
            throw new EntityNotFoundException("Image is not found.");
        }
        var fileBytes = await File.ReadAllBytesAsync(filePath);
        var contentType = $"image/{Path.GetExtension(fileName).TrimStart('.').ToLowerInvariant()}";
        return (fileBytes, contentType);
    }
    
    private async Task WriteFileAsync(IFormFile image, string imageFilePath)
    {
        await using (var stream = new FileStream(imageFilePath, FileMode.Create))
        {
            await image.CopyToAsync(stream); 
        }
    }
    
   
}