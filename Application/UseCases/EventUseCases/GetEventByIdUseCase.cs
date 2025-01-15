using Application.Contracts.RepositoryContracts;
using Application.Contracts.UseCaseContracts.EventUseCaseContracts;
using Application.DtoModels.EventsDto;
using Application.Exceptions;
using AutoMapper;
using Domain.Models;

namespace Application.UseCases.EventUseCases;

public class GetEventByIdUseCase :IGetEventByIdUseCase
{
    private readonly IRepositoriesManager _repositoriesManager;
    private readonly IMapper _mapper;
    private const string ControllerRoute = "events";
    private const string EndpointRoute = "images";

    public GetEventByIdUseCase(IRepositoriesManager repositoriesManager, IMapper mapper)
    {
        _repositoriesManager = repositoriesManager;
        _mapper = mapper;
    }
    
    public async Task<GetEventDto> Handle(Guid id, HttpRequest request)
    {
        var eventById = await _repositoriesManager.Events.GetByIdAsync(id);
        if (eventById == null)
            throw new EntityNotFoundException($"Event with id {id} doesn't exist");
        eventById = AttachLinkToImage(eventById, request, ControllerRoute, EndpointRoute);
        var eventDto = _mapper.Map<GetEventDto>(eventById);
        return eventDto;
    }
    
    private static Event AttachLinkToImage(Event item, HttpRequest request, string controllerRoute, string endpointRoute)
    {
        if (!string.IsNullOrEmpty(item.Image)) 
            item.Image = new Uri($"{request.Scheme}://{request.Host}/{controllerRoute}/{endpointRoute}/{item.Image}").ToString();
        return item;
    }
}