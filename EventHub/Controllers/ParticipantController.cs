using BusinessLayer.DtoModels.ParticipantDto;
using BusinessLayer.Services.Contracts;
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
    public async Task<IActionResult> GetAllParticipants(Guid eventId)
    {
        var participants = await _participantService.GetParticipantsAsync(eventId);
        return Ok(participants);
    }
    
    [HttpPost]
    public async Task<IActionResult> RegisterParticipant([FromBody]CreateParticipantDto item, Guid eventId)
    {
        var participant = await _participantService.RegisterParticipantAsync(eventId, item);
        return Ok(participant);
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