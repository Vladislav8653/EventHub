using BusinessLayer.DtoModels.ParticipantDto;
using Microsoft.AspNetCore.Mvc;

namespace EventHub.Controllers;

//[Route("events/{id:guid}/participants")]
//[ApiController]
public class ParticipantController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetParticipants(Guid id)
    {
        throw new NotImplementedException();
    }
    
    [HttpPost]
    public async Task<IActionResult> RegisterParticipant(Guid eventId, CreateParticipantDto item)
    {
        throw new NotImplementedException();
    }
    
    [HttpGet]
    public async Task<IActionResult> GetParticipant(Guid eventId, Guid participantId)
    {
        throw new NotImplementedException();
    }
    
    [HttpDelete]
    public async Task<IActionResult> RemoveParticipant(Guid eventId, Guid participantId)
    {
        throw new NotImplementedException();
    }
    
    
}