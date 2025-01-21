using Domain.RepositoryContracts;
using Application.Contracts.UseCaseContracts.EventUseCaseContracts;
using Application.DtoModels.EventsDto;
using Application.Exceptions;
using Application.Contracts.ImageServiceContracts;
using AutoMapper;
using Domain;
using Domain.Models;

namespace Application.UseCases.EventUseCases;

public class GetEventByIdUseCase : IGetEventByIdUseCase
{
    private readonly IRepositoriesManager _repositoriesManager;
    private readonly IMapper _mapper;

    public GetEventByIdUseCase(IRepositoriesManager repositoriesManager, IMapper mapper)
    {
        _repositoriesManager = repositoriesManager;
        _mapper = mapper;
    }
    
    public async Task<GetEventDto> Handle(Guid id, ImageUrlConfiguration request, CancellationToken cancellationToken)
    {
        var eventById = await _repositoriesManager.Events.GetByIdAsync(id, cancellationToken);
        if (eventById == null)
            throw new EntityNotFoundException($"Event with id {id} doesn't exist");
        eventById = AttachLinkToImage(eventById, request);
        var eventDto = _mapper.Map<GetEventDto>(eventById);
        return eventDto;
    }
    
    private static Event AttachLinkToImage(Event item, ImageUrlConfiguration request)
    {
        if (!string.IsNullOrEmpty(item.Image)) 
            item.Image = new Uri($"{request.BaseUrl}/{request.ControllerRoute}/{request.EndpointRoute}/{item.Image}").ToString();
        return item;
    }
}