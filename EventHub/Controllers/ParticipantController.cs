using System.Security.Claims;
using BusinessLayer.DtoModels.CommonDto;
using BusinessLayer.DtoModels.ParticipantDto;
using BusinessLayer.Services.Contracts;
using EventHub.Validation.CommonValidation;
using EventHub.Validation.Participants.Attributes;
using Microsoft.AspNetCore.Authorization;
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
    
    [Authorize]
    [HttpGet]
    [ServiceFilter(typeof(ValidatePageParamsAttribute))]
    public async Task<IActionResult> GetAllParticipants([FromQuery]PageParamsDto pageParams, Guid eventId)
    {
        var participants = await _participantService.GetParticipantsAsync(pageParams, eventId);
        return Ok(participants);
    }
    
    [Authorize]
    [HttpPost]
    [ServiceFilter(typeof(ValidateParticipantDtoAttribute))]
    public async Task<IActionResult> RegisterParticipant([FromBody]CreateParticipantDto item, Guid eventId)
    {
        // Извлекаем id пользователя из claims
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
        {
            return Unauthorized(); 
        }
        var userId = userIdClaim.Value; 
        var registration = await _participantService.RegisterParticipantAsync(eventId, item, userId);
        return Ok(registration);
    }
    
    [Authorize]
    [HttpGet("{participantId:guid}")]
    public async Task<IActionResult> GetParticipantById(Guid eventId, Guid participantId)
    {
        var participant = await _participantService.GetParticipantAsync(eventId, participantId);
        return Ok(participant);
    }
    
    
    
    [Authorize]
    [HttpDelete("{participantId:guid}")]
    public async Task<IActionResult> RemoveParticipant(Guid eventId, Guid participantId)
    {
        // Извлекаем id пользователя из claims
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
        {
            return Unauthorized(); 
        }
        var userId = userIdClaim.Value; 
        var participant = await _participantService.RemoveParticipantAsync(eventId, participantId, userId);
        return Ok(participant);
    }
    
    
}