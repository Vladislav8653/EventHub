using System.Security.Claims;
using Application.Contracts.UseCaseContracts.ParticipantUseCaseContracts;
using Application.DtoModels.CommonDto;
using Application.DtoModels.ParticipantDto;
using Application.Validation.CommonValidation;
using Application.Validation.Participants.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("events/{eventId:guid}/participants")]
[ApiController]
public class ParticipantController : ControllerBase
{
    private readonly ICreateParticipantUseCase _createParticipantUseCase;
    private readonly IDeleteParticipantUseCase _deleteParticipantUseCase;
    private readonly IGetAllParticipantsUseCase _getAllParticipantsUseCase;
    private readonly IGetParticipantUseCase _getParticipantUseCase;
    public ParticipantController(ICreateParticipantUseCase createParticipantUseCase, 
        IDeleteParticipantUseCase deleteParticipantUseCase, 
        IGetAllParticipantsUseCase getAllParticipantsUseCase, 
        IGetParticipantUseCase getParticipantUseCase)
    {
        _createParticipantUseCase = createParticipantUseCase;
        _deleteParticipantUseCase = deleteParticipantUseCase;
        _getAllParticipantsUseCase = getAllParticipantsUseCase;
        _getParticipantUseCase = getParticipantUseCase;
    }
    
    [Authorize]
    [HttpGet]
    [ServiceFilter(typeof(ValidatePageParamsAttribute))]
    public async Task<IActionResult> GetAllParticipants([FromQuery]PageParamsDto pageParams, Guid eventId)
    {
        var participants = await _getAllParticipantsUseCase.Handle(pageParams, eventId);
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
        var registration = await _createParticipantUseCase.Handle(eventId, item, userId);
        return Ok(registration);
    }
    
    [Authorize]
    [HttpGet("{participantId:guid}")]
    public async Task<IActionResult> GetParticipantById(Guid eventId, Guid participantId)
    {
        var participant = await _getParticipantUseCase.Handle(eventId, participantId);
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
        var participant = await _deleteParticipantUseCase.Handle(eventId, participantId, userId);
        return Ok(participant);
    }
    
    
}