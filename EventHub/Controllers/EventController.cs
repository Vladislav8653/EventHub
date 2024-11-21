using AutoMapper;
using BusinessLayer.DtoModels.EventsDto;
using BusinessLayer.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;

namespace EventHub.Controllers;

[ApiController]
[Route("events")]
public class EventController : ControllerBase
{
    private IEventService _eventService;
    private ICategoryService _categoryService;
    private IMapper _mapper;
    private IValidator<CreateEventDto> _validator;
    public EventController(IEventService eventService, IMapper mapper, 
        ICategoryService categoryService, IValidator<CreateEventDto> validator)
    {
        _eventService = eventService;
        _mapper = mapper;
        _categoryService = categoryService;
        _validator = validator;
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
    public async Task<IActionResult> CreateEvent([FromBody]CreateEventDto? item)
    {
        if (item == null)
            return BadRequest("Body is null");
        var result = await _validator.ValidateAsync(item);
        if (!result.IsValid)
        {
            var errors = result.Errors
                .GroupBy(vf => vf.PropertyName)
                .ToDictionary(g => g.Key, g => g.First().ErrorMessage);
            return BadRequest(errors);
        }
        var newEvent = await _eventService.CreateAsync(item);
        return Ok(newEvent);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEvent(Guid id, [FromBody] UpdateEventDto? item)
    {
        if (item == null)
        {
            return BadRequest();
        }
        var result = await _eventService.UpdateAsync(id, item);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEvent(Guid id)
    {
        var result = await _eventService.DeleteAsync(id);
        return Ok(result);
    }
    
    
    
    
}