using Application.Specifications.Filtering;
using Application.Specifications.Pagination;

namespace Application.Specifications.Dto;

public class EventQueryParams (EventFilters? filters, PageParams pageParams)
{
    public EventFilters? Filters { get; } = filters;
    public PageParams PageParams { get; } = pageParams;
}