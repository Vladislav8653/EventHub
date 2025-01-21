using Domain.RepositoryContracts;
using Application.Contracts.UseCaseContracts.EventUseCaseContracts;
using Application.DtoModels.EventsDto;
using Application.Exceptions;
using Application.ImageService;
using AutoMapper;
using Domain.Models;

namespace Application.UseCases.EventUseCases;

public class GetEventByNameUseCase : IGetEventByNameUseCase
{
    private readonly IRepositoriesManager _repositoriesManager;
    private readonly IMapper _mapper;

    public GetEventByNameUseCase(IRepositoriesManager repositoriesManager, IMapper mapper)
    {
        _repositoriesManager = repositoriesManager;
        _mapper = mapper;
    }
    
    public async Task<GetEventDto> Handle(string name, ImageUrlConfiguration request, CancellationToken cancellationToken)
    {
        var eventByName = await _repositoriesManager.Events.GetByNameAsync(name, cancellationToken);
        if (eventByName == null)
            throw new EntityNotFoundException($"Event with name {name} doesn't exist");
        eventByName = AttachLinkToImage(eventByName, request);
        var eventDto = _mapper.Map<GetEventDto>(eventByName);
        return eventDto;
    }
    
    private static Event AttachLinkToImage(Event item, ImageUrlConfiguration request)
    {
        if (!string.IsNullOrEmpty(item.Image)) 
            item.Image = new Uri($"{request.BaseUrl}/{request.ControllerRoute}/{request.EndpointRoute}/{item.Image}").ToString();
        return item;
    }
}