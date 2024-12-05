using BusinessLayer.DtoModels.CommonDto;
using BusinessLayer.DtoModels.ParticipantDto;
using BusinessLayer.Services.Contracts;
using EventHub.Validation.CommonValidation;
using Microsoft.AspNetCore.Mvc;

namespace EventHub.Controllers;

[Route("events/{eventId:guid}/participants")]
[ApiController]
public class ParticipantController : ControllerBase
{
    private readonly IParticipantService _participantService;
    public ParticipantController(IParticipantService participantService)
    {
        _participantService = participantService;
    }
    
    [HttpGet]
    [ServiceFilter(typeof(ValidatePageParamsAttribute))]
    public async Task<IActionResult> GetAllParticipants([FromQuery]PageParamsDto pageParams, Guid eventId)
    {
        var participants = await _participantService.GetParticipantsAsync(pageParams, eventId);
        return Ok(participants);
    }
    
    [HttpPost]
    public async Task<IActionResult> RegisterParticipant([FromBody]CreateParticipantDto item, Guid eventId)
    {
        var registration = await _participantService.RegisterParticipantAsync(eventId, item);
        return Ok(registration);
    }
    
    [HttpGet("{participantId:guid}")]
    public async Task<IActionResult> GetParticipantById(Guid eventId, Guid participantId)
    {
        var participant = await _participantService.GetParticipantAsync(eventId, participantId);
        return Ok(participant);
    }
    
    [HttpDelete("{participantId:guid}")]
    public async Task<IActionResult> RemoveParticipant(Guid eventId, Guid participantId)
    {
        var participant = await _participantService.RemoveParticipantAsync(eventId, participantId);
        return Ok(participant);
    }
    
    
}