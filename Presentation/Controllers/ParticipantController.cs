using Application.Contracts;
using Application.Contracts.AuthContracts;
using Application.Contracts.UseCaseContracts.ParticipantUseCaseContracts;
using Application.DtoModels.CommonDto;
using Application.DtoModels.ParticipantDto;
using Application.Validation;
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
    private readonly IJwtProvider _jwtProvider;
    private readonly ICookieService _cookieService;
    public ParticipantController(ICreateParticipantUseCase createParticipantUseCase, 
        IDeleteParticipantUseCase deleteParticipantUseCase, 
        IGetAllParticipantsUseCase getAllParticipantsUseCase, 
        IGetParticipantUseCase getParticipantUseCase, IJwtProvider jwtProvider, ICookieService cookieService)
    {
        _createParticipantUseCase = createParticipantUseCase;
        _deleteParticipantUseCase = deleteParticipantUseCase;
        _getAllParticipantsUseCase = getAllParticipantsUseCase;
        _getParticipantUseCase = getParticipantUseCase;
        _jwtProvider = jwtProvider;
        _cookieService = cookieService;
    }
    
    [Authorize]
    [HttpGet]
    [ValidateDtoServiceFilter<PageParamsDto>]
    public async Task<IActionResult> GetAllParticipants([FromQuery]PageParamsDto pageParams, Guid eventId, CancellationToken cancellationToken)
    {
        var participants = await _getAllParticipantsUseCase.Handle(pageParams, eventId, cancellationToken);
        return Ok(participants);
    }
    
    [Authorize]
    [HttpPost]
    [ValidateDtoServiceFilter<CreateParticipantDto>]
    public async Task<IActionResult> RegisterParticipant([FromBody]CreateParticipantDto item, Guid eventId, CancellationToken cancellationToken)
    {
        var userId = _jwtProvider.GetUserIdAccessToken(_cookieService.GetCookie(Request, UserController.AccessTokenCookieName));
        var registration = await _createParticipantUseCase.Handle(eventId, item, userId, cancellationToken);
        return Ok(registration);
    }
    
    [Authorize]
    [HttpGet("{participantId:guid}")]
    public async Task<IActionResult> GetParticipantById(Guid eventId, Guid participantId, CancellationToken cancellationToken)
    {
        var participant = await _getParticipantUseCase.Handle(eventId, participantId, cancellationToken);
        return Ok(participant);
    }
    
    
    
    [Authorize]
    [HttpDelete("{participantId:guid}")]
    public async Task<IActionResult> RemoveParticipant(Guid eventId, Guid participantId, CancellationToken cancellationToken)
    {
        var userId = _jwtProvider.GetUserIdAccessToken(_cookieService.GetCookie(Request, UserController.AccessTokenCookieName));
        var participant = await _deleteParticipantUseCase.Handle(eventId, participantId, userId, cancellationToken);
        return Ok(participant);
    }
    
    
}