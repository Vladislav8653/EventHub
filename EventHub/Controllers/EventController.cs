using BusinessLayer.DtoModels.EventsDto;
using BusinessLayer.Services.Contracts;
using EventHub.Validators.Filters;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;

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
        var events = await _eventService.GetAllAsync();
        return Ok(events);
    }
    
    [HttpGet("{id:guid}", Name = "GetEventById")]
    public async Task<IActionResult> GetEventById(Guid id)
    {
        var events = await _eventService.GetByIdAsync(id);
        return Ok(events);
    }
    
    [HttpGet("{name}", Name = "GetEventByName")]
    public async Task<IActionResult> GetEventByName(string name)
    {
        var events = await _eventService.GetByNameAsync(name);
        return Ok(events);
    }
    
    /*[HttpGet(Name = "GetEventByDate")]
    public async Task<IActionResult> GetEventByDate(string date)
    {
        var events = await _eventService.GetByDateAsync(date);
        return Ok(events);
    }
    
    [HttpGet(Name = "GetEventByDateRange")]
    public async Task<IActionResult> GetEventByDateRange(string startDate, string finishDate)
    {
        var events = await _eventService.GetByDateRangeAsync(startDate, finishDate);
        return Ok(events);
    }

    
    [HttpGet(Name = "GetEventByPlace")]
    public async Task<IActionResult> GetEventByPlace(string place)
    {
        var events = await _eventService.GetByPlaceAsync(place);
        return Ok(events);
    }
    
    [HttpGet(Name = "GetEventByCategory")]
    public async Task<IActionResult> GetEventByCategory(string categoryText)
    {
        var events = await _eventService.GetByCategoryAsync(categoryText);
        return Ok(events);
    }*/

    [HttpPost]
    [ServiceFilter(typeof(ValidateEventDtoAttribute))]
    public async Task<IActionResult> CreateEvent([FromBody]CreateEventDto item)
    {
        var newEvent = await _eventService.CreateAsync(item);
        return Ok(newEvent);
    }

    [HttpPut("{id}")]
    [ServiceFilter(typeof(ValidateEventDtoAttribute))]
    public async Task<IActionResult> UpdateEvent(Guid id, [FromBody] CreateEventDto item)
    {
        var updatedEvent = await _eventService.UpdateAsync(id, item);
        return Ok(updatedEvent);
    }

    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEvent(Guid id)
    {
        var result = await _eventService.DeleteAsync(id);
        return Ok(result);
    }
}