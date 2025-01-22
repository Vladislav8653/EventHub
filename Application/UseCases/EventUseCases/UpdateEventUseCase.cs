using Application.Contracts.ImageServiceContracts;
using Application.Contracts.UseCaseContracts.EventUseCaseContracts;
using Application.DtoModels.EventsDto;
using Application.Exceptions;
using AutoMapper;
using Domain;

namespace Application.UseCases.EventUseCases;

public class UpdateEventUseCase : IUpdateEventUseCase
{
    
    private readonly IRepositoriesManager _repositoriesManager;
    private readonly IMapper _mapper;
    private readonly IImageService _imageService;

    public UpdateEventUseCase(IRepositoriesManager repositoriesManager, IMapper mapper, IImageService imageService)
    {
        _repositoriesManager = repositoriesManager;
        _mapper = mapper;
        _imageService = imageService;
    }
    
    public async Task<GetEventDto> Handle(Guid id, CreateEventDto item, CancellationToken cancellationToken)
    {
        var eventToUpdate = await _repositoriesManager.Events.GetByIdAsync(id, cancellationToken);
        if (eventToUpdate == null)
            throw new EntityNotFoundException($"Event with id {id} doesn't exist");
        var category = await _repositoriesManager.Categories.TryGetByNameAsync(item.Category, cancellationToken);
        if (category == null)
            throw new EntityNotFoundException($"Category {item.Category} doesn't exits.");
        _mapper.Map(item, eventToUpdate);
        if (item.Image != null) 
        {
            await _imageService.WriteFileAsync(item.Image);
        }
        var oldFilename = eventToUpdate.Image;
        if (oldFilename != null)
        {
            _imageService.DeleteFile(oldFilename);
        }
        eventToUpdate.Image = item.Image?.FileName;
        eventToUpdate.CategoryId = category.Id;
        _repositoriesManager.Events.Update(eventToUpdate);
        await _repositoriesManager.SaveAsync();
        var eventDto = _mapper.Map<GetEventDto>(eventToUpdate);
        eventDto.Category = item.Category;
        return eventDto;
    }
}