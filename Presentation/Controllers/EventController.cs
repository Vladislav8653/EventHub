using Application.Contracts;
using Application.Contracts.AuthContracts;
using Application.Contracts.UseCaseContracts.EventUseCaseContracts;
using Application.DtoModels.EventsDto;
using Application.ImageService;
using Application.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route(ControllerRoute)]
public class EventController : ControllerBase
{
    private const string ControllerRoute = "events";
    private const string ImageEndpointRoute = "images";
    private readonly ImageUrlConfiguration _imageUrlConfiguration; // для формирования URL к изображению
    private readonly string _imageStoragePath; // для формирования пути к изображениям
    private readonly IImageService _imageService;  // сервис для работы с изображениями
    private readonly IJwtProvider _jwtProvider;
    private readonly ICookieService _cookieService;
    private readonly ICreateEventUseCase _createEventUseCase;
    private readonly IDeleteEventUseCase _deleteEventUseCase;
    private readonly IUpdateEventUseCase _updateEventUseCase;
    private readonly IGetAllEventsUseCase _getAllEventsUseCase;
    private readonly IGetEventByIdUseCase _getEventByIdUseCase;
    private readonly IGetEventByNameUseCase _getEventByNameUseCase;
    private readonly IGetAllUserEventsUseCase _getAllUserEventsUseCase;
    public EventController(IImageService imageService,
        IConfiguration configuration,
        ICreateEventUseCase createEventUseCase, 
        IDeleteEventUseCase deleteEventUseCase,
        IUpdateEventUseCase updateEventUseCase, 
        IGetEventByIdUseCase getEventByIdUseCase, 
        IGetAllEventsUseCase getAllEventsUseCase, 
        IGetEventByNameUseCase getEventByNameUseCase, 
        IWebHostEnvironment hostingEnvironment,
        IJwtProvider jwtProvider,
        ICookieService cookieService, IGetAllUserEventsUseCase getAllUserEventsUseCase)
    {
        _imageUrlConfiguration = InitializeImageUrlConfiguration(); 
        _imageStoragePath = InitializeImageStoragePath(configuration, hostingEnvironment); 
        _imageService = imageService;
        _createEventUseCase = createEventUseCase;
        _deleteEventUseCase = deleteEventUseCase;
        _updateEventUseCase = updateEventUseCase;
        _getEventByIdUseCase = getEventByIdUseCase;
        _getAllEventsUseCase = getAllEventsUseCase;
        _getEventByNameUseCase = getEventByNameUseCase;
        _jwtProvider = jwtProvider;
        _cookieService = cookieService;
        _getAllUserEventsUseCase = getAllUserEventsUseCase;
    }
    
    [HttpGet]
    [ValidateDtoServiceFilter<EventQueryParamsDto>]
    public async Task<IActionResult> GetAllEvents([FromQuery]EventQueryParamsDto eventParamsDto, CancellationToken cancellationToken)
    {
        var events = 
            await _getAllEventsUseCase.Handle(eventParamsDto, _imageUrlConfiguration, cancellationToken);
        return Ok(events);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetEventById(Guid id, CancellationToken cancellationToken)
    {
        var events = await _getEventByIdUseCase.Handle(id, _imageUrlConfiguration, cancellationToken);
        return Ok(events);
    }
    
    [HttpGet("{name}")]
    public async Task<IActionResult> GetEventByName(string name, CancellationToken cancellationToken)
    {
        var events = await _getEventByNameUseCase.Handle(name, _imageUrlConfiguration, cancellationToken);
        return Ok(events);
    }
    
    
    [Authorize]
    [HttpPost]
    [ValidateDtoServiceFilter<CreateEventDto>]
    public async Task<IActionResult> CreateEvent([FromForm]CreateEventDto item, CancellationToken cancellationToken)
    {
        var newEvent = await _createEventUseCase.Handle(item, _imageStoragePath, cancellationToken);
        return Ok(newEvent);
    }

    
    [Authorize]
    [HttpPut("{id:guid}")]
    [ValidateDtoServiceFilter<CreateEventDto>]
    public async Task<IActionResult> UpdateEvent([FromForm]CreateEventDto item, Guid id, CancellationToken cancellationToken)
    {
        var updatedEvent =  await _updateEventUseCase.Handle(id, item, _imageStoragePath, cancellationToken);
        return Ok(updatedEvent);
    }

    
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEvent(Guid id, CancellationToken cancellationToken)
    {
        var result = await _deleteEventUseCase.Handle(id, _imageStoragePath, cancellationToken);
        return Ok(result);
    }
    
    [Authorize]
    [HttpGet("my")]
    public async Task<IActionResult> GetAllUserEvents(CancellationToken cancellationToken)
    {
        var userId = _jwtProvider.GetUserIdAccessToken(_cookieService.GetCookie(Request, UserController.AccessTokenCookieName));
        var events = await _getAllUserEventsUseCase.Handle(userId, _imageUrlConfiguration, cancellationToken);
        return Ok(events);
    }
    
    
    [HttpGet(ImageEndpointRoute + "/{fileName}")]
    public async Task<IActionResult> GetImage(string fileName)
    {
        var (fileBytes, contentType) = await _imageService.GetImageAsync(fileName, _imageStoragePath);
        return File(fileBytes, contentType, fileName);
    }

    private ImageUrlConfiguration InitializeImageUrlConfiguration()
    {
        if (HttpContext == null)
            throw new InvalidOperationException("HttpContext is not available");
        var request = HttpContext.Request;
        return new ImageUrlConfiguration(request, ControllerRoute, ImageEndpointRoute);
    }

    private string InitializeImageStoragePath(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
    {
        var config = configuration["ImageStorage:wwwrootRelativePath"];
        if (config == null)
            throw new InvalidOperationException("Image storage path is not available.");
        var imagePath = Path.Combine(hostingEnvironment.WebRootPath, config);
        return imagePath;
    }
}