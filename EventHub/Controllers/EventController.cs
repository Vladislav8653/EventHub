using BusinessLayer.DtoModels.EventsDto;
using BusinessLayer.Services.Contracts;
using EventHub.Validators.Event;
using EventHub.Validators.Event.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace EventHub.Controllers;

[ApiController]
[Route("events")]
public class EventController : ControllerBase
{
    private readonly IEventService _eventService;
    public EventController(IEventService eventService)
    {
        _eventService = eventService;
    }
    
    
    [HttpGet(Name = "GetEvents")]
    public async Task<IActionResult> GetAllEvents()
    {
        var events = await _eventService.GetAllAsync(Request);
        return Ok(events);
    }
    
    [HttpGet("{id:guid}", Name = "GetEventById")]
    public async Task<IActionResult> GetEventById(Guid id)
    {
        var events = await _eventService.GetByIdAsync(id, Request);
        return Ok(events);
    }
    
    [HttpGet("{name}", Name = "GetEventByName")]
    public async Task<IActionResult> GetEventByName(string name)
    {
        var events = await _eventService.GetByNameAsync(name, Request);
        return Ok(events);
    }

    [HttpGet("filter", Name = "GetByFilters")]
    [ServiceFilter(typeof(ValidateEventFiltersDtoAttribute))]
    public async Task<IActionResult> GetFilteredEvents([FromQuery] EventFiltersDto filters) 
    {
        var events = await _eventService.GetByFiltersAsync(filters, Request);
        return Ok(events);
    }
    
    [HttpPost]
    [ServiceFilter(typeof(ValidateEventDtoAttribute))]
    public async Task<IActionResult> CreateEvent([FromForm]CreateEventDto item)
    {
        var newEvent = await _eventService.CreateAsync(item);
        return Ok(newEvent);
    }

    [HttpPut("{id:guid}")]
    [ServiceFilter(typeof(ValidateEventDtoAttribute))]
    public async Task<IActionResult> UpdateEvent([FromForm]CreateEventDto item, Guid id)
    {
        var updatedEvent =  await _eventService.UpdateAsync(id, item);
        return Ok(updatedEvent);
    }

    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEvent(Guid id)
    {
        var result = await _eventService.DeleteAsync(id);
        return Ok(result);
    }
    
    
    [HttpGet("images/{fileName}")]
    public async Task<IActionResult> GetImage(string fileName)
    {
        var (fileBytes, contentType) = await _eventService.GetImageAsync(fileName);
        return File(fileBytes, contentType, fileName);
    }
}