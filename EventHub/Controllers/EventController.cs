﻿using System.Security.Claims;
using BusinessLayer.DtoModels.EventsDto;
using BusinessLayer.Services.Contracts;
using EventHub.Validation.Event.Attributes;
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
    
    [HttpGet]
    [ServiceFilter(typeof(ValidateEventQueryParamsAttribute))]
    public async Task<IActionResult> GetAllEvents([FromQuery]EventQueryParamsDto eventParamsDto)
    {
        var events = await _eventService.GetAllEventsAsync(eventParamsDto, Request);
        return Ok(events);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetEventById(Guid id)
    {
        var events = await _eventService.GetByIdAsync(id, Request);
        return Ok(events);
    }
    
    [HttpGet("{name}")]
    public async Task<IActionResult> GetEventByName(string name)
    {
        var events = await _eventService.GetByNameAsync(name, Request);
        return Ok(events);
    }
    
    
    [Authorize]
    [HttpPost]
    [ServiceFilter(typeof(ValidateEventDtoAttribute))]
    public async Task<IActionResult> CreateEvent([FromForm]CreateEventDto item)
    {
        var newEvent = await _eventService.CreateAsync(item);
        return Ok(newEvent);
    }

    
    [Authorize]
    [HttpPut("{id:guid}")]
    [ServiceFilter(typeof(ValidateEventDtoAttribute))]
    public async Task<IActionResult> UpdateEvent([FromForm]CreateEventDto item, Guid id)
    {
        var updatedEvent =  await _eventService.UpdateAsync(id, item);
        return Ok(updatedEvent);
    }

    
    [Authorize]
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