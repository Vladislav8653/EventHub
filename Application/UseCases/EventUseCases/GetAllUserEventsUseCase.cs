using Application.Contracts.UseCaseContracts.EventUseCaseContracts;
using Application.DtoModels.EventsDto;
using Application.Contracts.ImageServiceContracts;
using AutoMapper;
using Domain;

namespace Application.UseCases.EventUseCases;

public class GetAllUserEventsUseCase : IGetAllUserEventsUseCase
{
    private readonly IRepositoriesManager _repositoriesManager;
    private readonly IMapper _mapper;
    private readonly IImageService _imageService;
    public GetAllUserEventsUseCase(IRepositoriesManager repositoriesManager, IMapper mapper, IImageService imageService)
    {
        _repositoriesManager = repositoriesManager;
        _mapper = mapper;
        _imageService = imageService;
    }
    
    public async Task<IEnumerable<GetEventDto>> Handle(Guid userId, ImageUrlConfiguration request, CancellationToken cancellationToken)
    {
        var rawEvents = await _repositoriesManager.Events.GetAllUserEventsAsync(userId, cancellationToken);
        var events = _imageService.AttachLinkToImage(rawEvents, request);
        var eventsWithImages = _mapper.Map<IEnumerable<GetEventDto>>(events);
        return eventsWithImages;
    }
}