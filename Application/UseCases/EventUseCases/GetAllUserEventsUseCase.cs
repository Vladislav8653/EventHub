using Domain.RepositoryContracts;
using Application.Contracts.UseCaseContracts.EventUseCaseContracts;
using Application.DtoModels.EventsDto;
using Application.ImageService;
using AutoMapper;
using Domain.Models;

namespace Application.UseCases.EventUseCases;

public class GetAllUserEventsUseCase : IGetAllUserEventsUseCase
{
    private readonly IRepositoriesManager _repositoriesManager;
    private readonly IMapper _mapper;
    public GetAllUserEventsUseCase(IRepositoriesManager repositoriesManager, IMapper mapper)
    {
        _repositoriesManager = repositoriesManager;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<GetEventDto>> Handle(Guid userId, ImageUrlConfiguration request, CancellationToken cancellationToken)
    {
        var rawEvents = await _repositoriesManager.Events.GetAllUserEventsAsync(userId, cancellationToken);
        var events = AttachLinkToImage(rawEvents, request);
        var eventsWithImages = _mapper.Map<IEnumerable<GetEventDto>>(events);
        return eventsWithImages;
    }
    
    private static List<Event> AttachLinkToImage(IEnumerable<Event> items, ImageUrlConfiguration request)
    {
        var itemsList = items.ToList();
        foreach (var item in itemsList.Where(item => !string.IsNullOrEmpty(item.Image)))
        {
            item.Image = new Uri($"{request.BaseUrl}/{request.ControllerRoute}/{request.EndpointRoute}/{item.Image}").ToString();
        }
        return itemsList;
    }
}