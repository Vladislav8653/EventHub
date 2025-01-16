using Application.Contracts.RepositoryContracts;
using Application.Contracts.UseCaseContracts.EventUseCaseContracts;
using Application.DtoModels.CommonDto;
using Application.DtoModels.EventsDto;
using Application.DtoModels.EventsDto.QueryParams;
using Application.Specifications;
using Application.Specifications.Dto;
using Application.Specifications.Filtering;
using Application.Specifications.Pagination;
using AutoMapper;
using Domain.Models;

namespace Application.UseCases.EventUseCases;

public class GetAllEventsUseCase : IGetAllEventsUseCase
{
    private readonly IRepositoriesManager _repositoriesManager;
    private readonly IMapper _mapper;
    private const int DefaultPage = 1;
    private const int DefaultPageSize = 5;
    
    public GetAllEventsUseCase(IRepositoriesManager repositoriesManager, IMapper mapper)
    {
        _repositoriesManager = repositoriesManager;
        _mapper = mapper;
    }
    
    public async Task<PagedResult<GetEventDto>> Handle(EventQueryParamsDto eventParamsDto, ImageUrlConfiguration request)
    {
        var filters = await GetFiltersFromQueryParams(eventParamsDto.Filters);
        var pageParamsDto = eventParamsDto.PageParams;
        var pageParams = pageParamsDto == null ? new PageParams(DefaultPage, DefaultPageSize) : 
            new PageParams(pageParamsDto.Page ?? DefaultPage, pageParamsDto.PageSize ?? DefaultPageSize);
        var eventParams = new EventQueryParams(filters, pageParams);
        var pagedResult = await _repositoriesManager.Events.GetAllByParamsAsync(eventParams);
        var events = AttachLinkToImage(pagedResult.Items, request);
        var eventsWithImages = _mapper.Map<IEnumerable<GetEventDto>>(events);
        return new PagedResult<GetEventDto>(eventsWithImages, pagedResult.Total);
    }
    
    private async Task<EventFilters?> GetFiltersFromQueryParams(EventFiltersDto? filtersDto)
    {
        if (filtersDto == null) return null;
        EventFilters filters = _mapper.Map<EventFilters>(filtersDto);
        if (filtersDto.Category == null) return filters;
        var category = await _repositoriesManager.Categories.TryGetByNameAsync(filtersDto.Category);
        if (category != null)
        {
            filters.Category = category;
        }
        return filters;
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