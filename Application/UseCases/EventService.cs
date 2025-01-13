using Application.Contracts;
using Application.Contracts.UseCaseContracts;
using Application.DtoModels.CommonDto;
using Application.DtoModels.EventsDto;
using Application.DtoModels.EventsDto.QueryParams;
using Application.Exceptions;
using Application.Specifications.Dto;
using Application.Specifications.Filtering;
using Application.Specifications.Pagination;
using AutoMapper;
using Domain.Models;

namespace Application.UseCases;

public class EventService : IEventService
{
    private readonly IRepositoriesManager _repositoriesManager;
    private readonly IMapper _mapper;
    private const int DefaultPage = 1;
    private const int DefaultPageSize = 5;
    private const string ControllerRoute = "events";
    private const string EndpointRoute = "images";

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
        eventById = AttachLinkToImage(eventById, request, ControllerRoute, EndpointRoute);
        var eventDto = _mapper.Map<GetEventDto>(eventById);
        return eventDto;
    }

    public async Task<GetEventDto> GetByNameAsync(string name, HttpRequest request)
    {
        var eventByName = await _repositoriesManager.Events.GetByNameAsync(name);
        if (eventByName == null)
            throw new EntityNotFoundException($"Event with name {name} doesn't exist");
        eventByName = AttachLinkToImage(eventByName, request, ControllerRoute, EndpointRoute);
        var eventDto = _mapper.Map<GetEventDto>(eventByName);
        return eventDto;
    }
    
    public async Task<EntitiesWithTotalCountDto<GetEventDto>> GetAllAsync(EventQueryParamsDto eventParamsDto, HttpRequest request)
    {
        var filters = await GetFiltersFromQueryParams(eventParamsDto.Filters);
        var pageParams = GetPageParamsFromQueryParams(eventParamsDto.PageParams, DefaultPage, DefaultPageSize); 
        var eventParams = new EventQueryParams
        {
            Filters = filters,
            PageParams = pageParams
        };
        var (events, totalFields) = await _repositoriesManager.Events.GetAllByParamsAsync(eventParams);
        events = AttachLinkToImage(events, request, ControllerRoute, EndpointRoute);
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
            filename = item.Image.FileName;
            var imageFilePath = Path.Combine("wwwroot", "images", filename);
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
        
        string? newFileName = null;
        if (item.Image != null) 
        {
            newFileName = item.Image.FileName;
            var imageFilePath = Path.Combine("wwwroot", "images", newFileName);
            await WriteFileAsync(item.Image, imageFilePath);
        }
        var oldFilename = eventToUpdate.Image;
        if (oldFilename != null)
        {
            var imageFilePath = Path.Combine("wwwroot", "images", oldFilename);
            DeleteFile(imageFilePath);
        }
        eventToUpdate.Image = newFileName;
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
        var filename = eventToDelete.Image;
        if (filename != null)
        {
            var imageFilePath = Path.Combine("wwwroot", "images", filename);
            DeleteFile(imageFilePath);
        }
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
    
    private static async Task WriteFileAsync(IFormFile image, string filePath)
    {
        await using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await image.CopyToAsync(stream); 
        }
    }

    private static void DeleteFile(string filePath)
    {
        File.Delete(filePath);
    }    

    private static Event AttachLinkToImage(Event item, HttpRequest request, string controllerRoute, string endpointRoute)
    {
        if (!string.IsNullOrEmpty(item.Image)) 
            item.Image = new Uri($"{request.Scheme}://{request.Host}/{controllerRoute}/{endpointRoute}/{item.Image}").ToString();
        return item;
    }
    private static List<Event> AttachLinkToImage(IEnumerable<Event> items, HttpRequest request,  string controllerRoute, string endpointRoute)
    {
        var itemsList = items.ToList();
        foreach (var item in itemsList.Where(item => !string.IsNullOrEmpty(item.Image)))
        {
            item.Image = new Uri($"{request.Scheme}://{request.Host}/{controllerRoute}/{endpointRoute}/{item.Image}").ToString();
        }
        return itemsList;
    }

    private async Task<EventFilters?> GetFiltersFromQueryParams(EventFiltersDto? filtersDto)
    {
        if (filtersDto == null) return null;
        EventFilters filters = _mapper.Map<EventFilters>(filtersDto);
        if (filtersDto.Category == null) return filters;
        var category = await _repositoriesManager.Categories.TryGetByNameAsync(filtersDto.Category);
        if (category != null)
        {
            filters.Category = category;
        }
        return filters;
    }

    private static PageParams? GetPageParamsFromQueryParams(PageParamsDto? pageParamsDro, int defaultPage, int defaultPageSize)
    {
        if (pageParamsDro == null) return null;
        PageParams pageParams = new PageParams(
            pageParamsDro.Page,
            pageParamsDro.PageSize,
            defaultPage,
            defaultPageSize);
        return pageParams;
    }
}