using Application;
using Application.Contracts;
using Application.Contracts.UseCaseContracts.EventUseCaseContracts;
using Application.DtoModels.EventsDto;
using Application.Specifications;
using Application.Validation.Event.Attributes;
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
    private readonly ICreateEventUseCase _createEventUseCase;
    private readonly IDeleteEventUseCase _deleteEventUseCase;
    private readonly IUpdateEventUseCase _updateEventUseCase;
    private readonly IGetAllEventsUseCase _getAllEventsUseCase;
    private readonly IGetEventByIdUseCase _getEventByIdUseCase;
    private readonly IGetEventByNameUseCase _getEventByNameUseCase;
    public EventController(IHttpContextAccessor httpContextAccessor, 
        IImageService imageService,
        IConfiguration configuration,
        ICreateEventUseCase createEventUseCase, 
        IDeleteEventUseCase deleteEventUseCase,
        IUpdateEventUseCase updateEventUseCase, 
        IGetEventByIdUseCase getEventByIdUseCase, 
        IGetAllEventsUseCase getAllEventsUseCase, 
        IGetEventByNameUseCase getEventByNameUseCase, 
        IWebHostEnvironment hostingEnvironment)
    {
        _imageUrlConfiguration = InitializeImageUrlConfiguration(httpContextAccessor); 
        _imageStoragePath = InitializeImageStoragePath(configuration, hostingEnvironment); 
        _imageService = imageService;
        _createEventUseCase = createEventUseCase;
        _deleteEventUseCase = deleteEventUseCase;
        _updateEventUseCase = updateEventUseCase;
        _getEventByIdUseCase = getEventByIdUseCase;
        _getAllEventsUseCase = getAllEventsUseCase;
        _getEventByNameUseCase = getEventByNameUseCase;
    }
    
    [HttpGet]
    [ServiceFilter(typeof(ValidateEventQueryParamsAttribute))]
    public async Task<IActionResult> GetAllEvents([FromQuery]EventQueryParamsDto eventParamsDto)
    {
        var events = 
            await _getAllEventsUseCase.Handle(eventParamsDto, _imageUrlConfiguration);
        return Ok(events);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetEventById(Guid id)
    {
        var events = await _getEventByIdUseCase.Handle(id, _imageUrlConfiguration);
        return Ok(events);
    }
    
    [HttpGet("{name}")]
    public async Task<IActionResult> GetEventByName(string name)
    {
        var events = await _getEventByNameUseCase.Handle(name, _imageUrlConfiguration);
        return Ok(events);
    }
    
    
    [Authorize]
    [HttpPost]
    [ServiceFilter(typeof(ValidateEventDtoAttribute))]
    public async Task<IActionResult> CreateEvent([FromForm]CreateEventDto item)
    {
        var newEvent = await _createEventUseCase.Handle(item, _imageStoragePath);
        return Ok(newEvent);
    }

    
    [Authorize]
    [HttpPut("{id:guid}")]
    [ServiceFilter(typeof(ValidateEventDtoAttribute))]
    public async Task<IActionResult> UpdateEvent([FromForm]CreateEventDto item, Guid id)
    {
        var updatedEvent =  await _updateEventUseCase.Handle(id, item, _imageStoragePath);
        return Ok(updatedEvent);
    }

    
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEvent(Guid id)
    {
        var result = await _deleteEventUseCase.Handle(id, _imageStoragePath);
        return Ok(result);
    }
    
    
    [HttpGet(ImageEndpointRoute + "/{fileName}")]
    public async Task<IActionResult> GetImage(string fileName)
    {
        var (fileBytes, contentType) = await _imageService.GetImageAsync(fileName, _imageStoragePath);
        return File(fileBytes, contentType, fileName);
    }

    private ImageUrlConfiguration InitializeImageUrlConfiguration(IHttpContextAccessor httpContextAccessor)
    {
        var httpContext = httpContextAccessor.HttpContext;
        if (httpContext == null)
            throw new InvalidOperationException("HttpContext is not available");
        var request = httpContext.Request;
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