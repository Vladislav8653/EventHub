using Application.Specifications.Filtering;
using Application.Specifications.Pagination;

namespace Application.Specifications.Dto;

public class EventQueryParams
{
    public EventFilters? Filters { get; set; }
    public PageParams? PageParams { get; set; }
}