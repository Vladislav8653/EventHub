using Application.Contracts.UseCaseContracts.EventUseCaseContracts;
using Application.DtoModels.EventsDto;
using Application.Exceptions;
using Application.Contracts.ImageServiceContracts;
using AutoMapper;
using Domain;

namespace Application.UseCases.EventUseCases;

public class GetEventByNameUseCase : IGetEventByNameUseCase
{
    private readonly IRepositoriesManager _repositoriesManager;
    private readonly IMapper _mapper;
    private readonly IImageService _imageService;

    public GetEventByNameUseCase(IRepositoriesManager repositoriesManager, IMapper mapper, IImageService imageService)
    {
        _repositoriesManager = repositoriesManager;
        _mapper = mapper;
        _imageService = imageService;
    }
    
    public async Task<GetEventDto> Handle(string name, ImageUrlConfiguration request, CancellationToken cancellationToken)
    {
        var eventByName = await _repositoriesManager.Events.GetByNameAsync(name, cancellationToken);
        if (eventByName == null)
            throw new EntityNotFoundException($"Event with name {name} doesn't exist");
        eventByName = _imageService.AttachLinkToImage(eventByName, request);
        var eventDto = _mapper.Map<GetEventDto>(eventByName);
        return eventDto;
    }
    
}