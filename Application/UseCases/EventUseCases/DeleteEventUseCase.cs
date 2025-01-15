using Application.Contracts.RepositoryContracts;
using Application.Contracts.UseCaseContracts.EventUseCaseContracts;
using Application.DtoModels.EventsDto;
using Application.Exceptions;
using AutoMapper;

namespace Application.UseCases.EventUseCases;

public class DeleteEventUseCase : IDeleteEventUseCase
{
    private readonly IRepositoriesManager _repositoriesManager;
    private readonly IMapper _mapper;
    public DeleteEventUseCase(IRepositoriesManager repositoriesManager, IMapper mapper) 
    {
        _repositoriesManager = repositoriesManager;
        _mapper = mapper;
    }

    public async Task<GetEventDto> Handle(Guid id)
    {
        var eventToDelete = await _repositoriesManager.Events.GetByIdAsync(id);
        if (eventToDelete == null)
            throw new EntityNotFoundException($"Event with id {id} doesn't exist");
        var filename = eventToDelete.Image;
        if (filename != null)
        {
            var imageFilePath = Path.Combine("wwwroot", "images", filename);
            DeleteFile(imageFilePath);
        }
        _repositoriesManager.Events.Delete(eventToDelete);
        await _repositoriesManager.SaveAsync();
        var eventDto = _mapper.Map<GetEventDto>(eventToDelete);
        return eventDto;
    }
}