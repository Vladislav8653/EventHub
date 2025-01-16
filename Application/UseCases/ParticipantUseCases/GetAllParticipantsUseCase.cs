using Application.Contracts.RepositoryContracts;
using Application.Contracts.UseCaseContracts.ParticipantUseCaseContracts;
using Application.DtoModels.CommonDto;
using Application.DtoModels.ParticipantDto;
using Application.Exceptions;
using Application.Specifications.Pagination;
using AutoMapper;

namespace Application.UseCases.ParticipantUseCases;

public class GetAllParticipantsUseCase : IGetAllParticipantsUseCase
{
    private readonly IRepositoriesManager _repositoriesManager;
    private readonly IMapper _mapper;
    private const int DefaultPage = 1;
    private const int DefaultPageSize = 5;
    
    public GetAllParticipantsUseCase(IRepositoriesManager repositoriesManager, IMapper mapper)
    {
        _repositoriesManager = repositoriesManager;
        _mapper = mapper;
    }
    
    public async Task<PagedResult<GetParticipantDto>> Handle(PageParamsDto? pageParamsDto, Guid eventId)
    {
        var eventDb = await _repositoriesManager.Events.GetByIdAsync(eventId);
        if (eventDb == null)
            throw new EntityNotFoundException($"Event with id {eventId} doesn't exist");
        
        var pageParams = pageParamsDto == null ? new PageParams(DefaultPage, DefaultPageSize) : 
            new PageParams(pageParamsDto.Page ?? DefaultPage, pageParamsDto.PageSize ?? DefaultPageSize);
        
        var participants = await _repositoriesManager.Participants.GetParticipantsAsync(pageParams, eventId);
        var participantsDto = _mapper.Map<IEnumerable<GetParticipantDto>>(participants.Items);
        return new PagedResult<GetParticipantDto>(participantsDto, participants.Total);
    }
}