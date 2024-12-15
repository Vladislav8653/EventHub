using AutoMapper;
using BusinessLayer.DtoModels.CommonDto;
using BusinessLayer.DtoModels.EventsDto;
using BusinessLayer.Exceptions;
using BusinessLayer.Services.Contracts;
using DataLayer.Models;
using DataLayer.Repositories.UnitOfWork;
using DataLayer.Specifications.Dto;
using DataLayer.Specifications.Filtering;
using DataLayer.Specifications.Pagination;
using Microsoft.AspNetCore.Http;


namespace BusinessLayer.Services.Implementations;

public class EventService : IEventService
{
    private readonly IRepositoriesManager _repositoriesManager;
    private readonly IMapper _mapper;
    private const int DefaultPage = 1;
    private const int DefaultPageSize = 5;
    public EventService(IRepositoriesManager repositoriesManager, IMapper mapper)
    {
        _repositoriesManager = repositoriesManager;
        _mapper = mapper;
    }
    

    public async Task<GetEventDto> GetByIdAsync(Guid id, HttpRequest request)
    {
        var eventById = await _repositoriesManager.Events.GetByIdAsync(id);
        if (eventById == null)
            throw new EntityNotFoundException($"Event with id {id} doesn't exist");
        eventById = AttachLinkToImage(eventById, request);
        var eventDto = _mapper.Map<GetEventDto>(eventById);
        return eventDto;
    }

    public async Task<GetEventDto> GetByNameAsync(string name, HttpRequest request)
    {
        var eventByName = await _repositoriesManager.Events.GetByNameAsync(name);
        if (eventByName == null)
            throw new EntityNotFoundException($"Event with name {name} doesn't exist");
        eventByName = AttachLinkToImage(eventByName, request);
        var eventDto = _mapper.Map<GetEventDto>(eventByName);
        return eventDto;
    }
    
    public async Task<EntitiesWithTotalCountDto<GetEventDto>> GetAllEventsAsync(EventQueryParamsDto eventParamsDto, HttpRequest request)
    {
        EventFilters? filters = null;
        if (eventParamsDto.Filters != null)
        {
            filters = _mapper.Map<EventFilters>(eventParamsDto.Filters);
            if (eventParamsDto.Filters.Category != null)
            {
                var category = await _repositoriesManager.Categories.TryGetByNameAsync(eventParamsDto.Filters.Category);
                if (category != null)
                {
                    filters.Category = category;
                }
            }
        }

        PageParams? pageParams = null;
        if (eventParamsDto.PageParams != null)
        { 
            pageParams = new PageParams(
                eventParamsDto.PageParams.Page,
                eventParamsDto.PageParams.PageSize,
                DefaultPage,
                DefaultPageSize);
        }
        
        var eventParams = new EventQueryParams
        {
            Filters = filters,
            PageParams = pageParams
        };
        
        var (events, totalFields) = await _repositoriesManager.Events.GetAllByParamsAsync(eventParams);
        events = AttachLinkToImage(events, request);
        var eventsWithImages = _mapper.Map<IEnumerable<GetEventDto>>(events);
        return new EntitiesWithTotalCountDto<GetEventDto>(eventsWithImages, totalFields);
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
        eventDto.Category = item.Category;
        return eventDto;
    }

    public async Task<GetEventDto> UpdateAsync(Guid id, CreateEventDto item) 
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
        eventDto.Category = item.Category;
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

    private Event AttachLinkToImage(Event item, HttpRequest request)
    {
        if (!string.IsNullOrEmpty(item.Image)) 
            item.Image = new Uri($"{request.Scheme}://{request.Host}/events/images/{item.Image}").ToString();
        return item;
    }
    private IEnumerable<Event> AttachLinkToImage(IEnumerable<Event> items, HttpRequest request)
    {
        var itemsList = items.ToList();
        foreach(var item in itemsList)
        {
            if (!string.IsNullOrEmpty(item.Image)) 
                item.Image = new Uri($"{request.Scheme}://{request.Host}/events/images/{item.Image}").ToString();
        }
        return itemsList;
    }
    
   
}