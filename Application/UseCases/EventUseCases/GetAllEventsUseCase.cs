using Application.Contracts.UseCaseContracts.EventUseCaseContracts;
using Application.DtoModels.CommonDto;
using Application.DtoModels.EventsDto;
using Application.DtoModels.EventsDto.QueryParams;
using Application.Contracts.ImageServiceContracts;
using AutoMapper;
using Domain;
using Domain.DTOs;

namespace Application.UseCases.EventUseCases;

public class GetAllEventsUseCase : IGetAllEventsUseCase
{
    private readonly IRepositoriesManager _repositoriesManager;
    private readonly IMapper _mapper;
    private readonly IImageService _imageService;
    private const int DefaultPage = 1;
    private const int DefaultPageSize = 5;
    
    public GetAllEventsUseCase(IRepositoriesManager repositoriesManager, IMapper mapper, IImageService imageService)
    {
        _repositoriesManager = repositoriesManager;
        _mapper = mapper;
        _imageService = imageService;
    }
    
    public async Task<PagedResult<GetEventDto>> Handle(EventQueryParamsDto eventParamsDto, ImageUrlConfiguration request, CancellationToken cancellationToken)
    {
        var filters = await GetFiltersFromQueryParams(eventParamsDto.Filters, cancellationToken);
        var pageParamsDto = eventParamsDto.PageParams;
        var pageParams = pageParamsDto == null ? new PageParams(DefaultPage, DefaultPageSize) : 
            new PageParams(pageParamsDto.Page ?? DefaultPage, pageParamsDto.PageSize ?? DefaultPageSize);
        var eventParams = new EventQueryParams(filters, pageParams);
        var pagedResult = await _repositoriesManager.Events.GetAllByParamsAsync(eventParams, cancellationToken);
        var events = _imageService.AttachLinkToImage(pagedResult.Items, request);
        var eventsWithImages = _mapper.Map<IEnumerable<GetEventDto>>(events);
        return new PagedResult<GetEventDto>(eventsWithImages, pagedResult.Total);
    }
    
    private async Task<EventFilters?> GetFiltersFromQueryParams(EventFiltersDto? filtersDto, CancellationToken cancellationToken)
    {
        if (filtersDto == null) return null;
        var filters = _mapper.Map<EventFilters>(filtersDto);
        if (filtersDto.Category == null) return filters;
        var category = await _repositoriesManager.Categories.TryGetByNameAsync(filtersDto.Category, cancellationToken);
        if (category != null)
        {
            filters.Category = category;
        }
        return filters;
    }
}