using Application.Contracts;
using Application.Contracts.ImageServiceContracts;
using Domain.RepositoryContracts;
using Application.Contracts.UseCaseContracts.EventUseCaseContracts;
using Application.DtoModels.EventsDto;
using Application.Exceptions;
using AutoMapper;
using Domain;
using Domain.Models;

namespace Application.UseCases.EventUseCases;

public class CreateEventUseCase : ICreateEventUseCase
{
    private readonly IRepositoriesManager _repositoriesManager;
    private readonly IMapper _mapper;
    private readonly IImageService _imageService;

    public CreateEventUseCase(IRepositoriesManager repositoriesManager, IMapper mapper, IImageService imageService)
    {
        _repositoriesManager = repositoriesManager;
        _mapper = mapper;
        _imageService = imageService;
    }
    
    public async Task<GetEventDto> Handle(CreateEventDto item, string imageStoragePath, CancellationToken cancellationToken)
    {
        var isUniqueName = await _repositoriesManager.Events.IsUniqueNameAsync(item.Name, cancellationToken);
        if (!isUniqueName)
            throw new EntityAlreadyExistException(nameof(Event), "name", item.Name);
        var category = await _repositoriesManager.Categories.TryGetByNameAsync(item.Category, cancellationToken);
        if (category == null)
            throw new EntityNotFoundException($"Category {item.Category} doesn't exits.");
        var eventForDb = _mapper.Map<Event>(item);
        
        string? filename = null;
        if (item.Image != null) 
        {
            filename = item.Image.FileName;
            var imageFilePath = Path.Combine(imageStoragePath, filename);
            await _imageService.WriteFileAsync(item.Image, imageFilePath);
        }
        eventForDb.Image = filename;
        eventForDb.CategoryId = category.Id;
        
        await _repositoriesManager.Events.CreateAsync(eventForDb, cancellationToken);
        await _repositoriesManager.SaveAsync();
        var eventDto = _mapper.Map<GetEventDto>(eventForDb);
        eventDto.Category = item.Category;
        return eventDto;
    }
}