using Application.Contracts.RepositoryContracts;
using Application.Contracts.UseCaseContracts.EventUseCaseContracts;
using Application.DtoModels.EventsDto;
using Application.Exceptions;
using AutoMapper;
using Domain.Models;

namespace Application.UseCases.EventUseCases;

public class GetEventByNameUseCase :IGetEventByNameUseCase
{
    private readonly IRepositoriesManager _repositoriesManager;
    private readonly IMapper _mapper;
    private const string ControllerRoute = "events";
    private const string EndpointRoute = "images";

    public GetEventByNameUseCase(IRepositoriesManager repositoriesManager, IMapper mapper)
    {
        _repositoriesManager = repositoriesManager;
        _mapper = mapper;
    }
    
    public async Task<GetEventDto> Handle(string name, HttpRequest request)
    {
        var eventByName = await _repositoriesManager.Events.GetByNameAsync(name);
        if (eventByName == null)
            throw new EntityNotFoundException($"Event with name {name} doesn't exist");
        eventByName = AttachLinkToImage(eventByName, request, ControllerRoute, EndpointRoute);
        var eventDto = _mapper.Map<GetEventDto>(eventByName);
        return eventDto;
    }
    
    private static Event AttachLinkToImage(Event item, HttpRequest request, string controllerRoute, string endpointRoute)
    {
        if (!string.IsNullOrEmpty(item.Image)) 
            item.Image = new Uri($"{request.Scheme}://{request.Host}/{controllerRoute}/{endpointRoute}/{item.Image}").ToString();
        return item;
    }
}