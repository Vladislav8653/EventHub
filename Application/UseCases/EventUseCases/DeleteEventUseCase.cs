using Application.Contracts.ImageServiceContracts;
using Application.Contracts.UseCaseContracts.EventUseCaseContracts;
using Application.DtoModels.EventsDto;
using Application.Exceptions;
using AutoMapper;
using Domain;

namespace Application.UseCases.EventUseCases;

public class DeleteEventUseCase : IDeleteEventUseCase
{
    private readonly IRepositoriesManager _repositoriesManager;
    private readonly IMapper _mapper;
    private readonly IImageService _imageService;
    public DeleteEventUseCase(IRepositoriesManager repositoriesManager, IMapper mapper, IImageService imageService) 
    {
        _repositoriesManager = repositoriesManager;
        _mapper = mapper;
        _imageService = imageService;
    }

    public async Task<GetEventDto> Handle(Guid id, CancellationToken cancellationToken)
    {
        var eventToDelete = await _repositoriesManager.Events.GetByIdAsync(id, cancellationToken);
        if (eventToDelete == null)
            throw new EntityNotFoundException($"Event with id {id} doesn't exist");
        var filename = eventToDelete.Image;
        if (filename != null)
        {
            _imageService.DeleteFile(filename);
        }
        _repositoriesManager.Events.Delete(eventToDelete);
        await _repositoriesManager.SaveAsync();
        var eventDto = _mapper.Map<GetEventDto>(eventToDelete);
        return eventDto;
    }
}