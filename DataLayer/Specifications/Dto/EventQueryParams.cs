using DataLayer.Specifications.Filtering;
using DataLayer.Specifications.Pagination;

namespace DataLayer.Specifications.Dto;

public class EventQueryParams
{
    public EventFilters? Filters { get; set; }
    public PageParams? PageParams { get; set; }
}