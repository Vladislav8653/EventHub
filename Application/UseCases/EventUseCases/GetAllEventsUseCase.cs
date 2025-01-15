using Application.Contracts;
using Application.Contracts.RepositoryContracts;
using Application.Contracts.UseCaseContracts.EventUseCaseContracts;
using Application.DtoModels.CommonDto;
using Application.DtoModels.EventsDto;
using Application.DtoModels.EventsDto.QueryParams;
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
    private const string ControllerRoute = "events";
    private const string EndpointRoute = "images";
    
    public GetAllEventsUseCase(IRepositoriesManager repositoriesManager, IMapper mapper)
    {
        _repositoriesManager = repositoriesManager;
        _mapper = mapper;
    }
    
    public async Task<EntitiesWithTotalCountDto<GetEventDto>> Handle(EventQueryParamsDto eventParamsDto, HttpRequest request)
    {
        var filters = await GetFiltersFromQueryParams(eventParamsDto.Filters);
        var pageParams = GetPageParamsFromQueryParams(eventParamsDto.PageParams, DefaultPage, DefaultPageSize); 
        var eventParams = new EventQueryParams
        {
            Filters = filters,
            PageParams = pageParams
        };
        var (events, totalFields) = await _repositoriesManager.Events.GetAllByParamsAsync(eventParams);
        events = AttachLinkToImage(events, request, ControllerRoute, EndpointRoute);
        var eventsWithImages = _mapper.Map<IEnumerable<GetEventDto>>(events);
        return new EntitiesWithTotalCountDto<GetEventDto>(eventsWithImages, totalFields);
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

    private static PageParams? GetPageParamsFromQueryParams(PageParamsDto? pageParamsDro, int defaultPage, int defaultPageSize)
    {
        if (pageParamsDro == null) return null;
        PageParams pageParams = new PageParams(
            pageParamsDro.Page,
            pageParamsDro.PageSize,
            defaultPage,
            defaultPageSize);
        return pageParams;
    }
    
    private static List<Event> AttachLinkToImage(IEnumerable<Event> items, HttpRequest request,  string controllerRoute, string endpointRoute)
    {
        var itemsList = items.ToList();
        foreach (var item in itemsList.Where(item => !string.IsNullOrEmpty(item.Image)))
        {
            item.Image = new Uri($"{request.Scheme}://{request.Host}/{controllerRoute}/{endpointRoute}/{item.Image}").ToString();
        }
        return itemsList;
    }
}