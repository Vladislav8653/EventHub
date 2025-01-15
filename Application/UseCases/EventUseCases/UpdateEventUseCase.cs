using Application.Contracts.RepositoryContracts;
using Application.Contracts.UseCaseContracts.EventUseCaseContracts;
using Application.DtoModels.EventsDto;
using Application.Exceptions;
using AutoMapper;

namespace Application.UseCases.EventUseCases;

public class UpdateEventUseCase : IUpdateEventUseCase
{
    
    private readonly IRepositoriesManager _repositoriesManager;
    private readonly IMapper _mapper;

    public UpdateEventUseCase(IRepositoriesManager repositoriesManager, IMapper mapper)
    {
        _repositoriesManager = repositoriesManager;
        _mapper = mapper;
    }
    
    public async Task<GetEventDto> Handle(Guid id, CreateEventDto item)
    {
        var eventToUpdate = await _repositoriesManager.Events.GetByIdAsync(id);
        if (eventToUpdate == null)
            throw new EntityNotFoundException($"Event with id {id} doesn't exist");
        var category = await _repositoriesManager.Categories.TryGetByNameAsync(item.Category);
        if (category == null)
            throw new EntityNotFoundException($"Category {item.Category} doesn't exits.");
        _mapper.Map(item, eventToUpdate);
        
        string? newFileName = null;
        if (item.Image != null) 
        {
            newFileName = item.Image.FileName;
            var imageFilePath = Path.Combine("wwwroot", "images", newFileName);
            await WriteFileAsync(item.Image, imageFilePath);
        }
        var oldFilename = eventToUpdate.Image;
        if (oldFilename != null)
        {
            var imageFilePath = Path.Combine("wwwroot", "images", oldFilename);
            DeleteFile(imageFilePath);
        }
        eventToUpdate.Image = newFileName;
        eventToUpdate.CategoryId = category.Id;
        
        _repositoriesManager.Events.Update(eventToUpdate);
        await _repositoriesManager.SaveAsync();
        var eventDto = _mapper.Map<GetEventDto>(eventToUpdate);
        eventDto.Category = item.Category;
        return eventDto;
    }
}