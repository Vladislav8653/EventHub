using Application.DtoModels.CommonDto;

namespace Application.DtoModels.EventsDto;

public class EventQueryParams (EventFilters? filters, PageParams pageParams)
{
    public EventFilters? Filters { get; } = filters;
    public PageParams PageParams { get; } = pageParams;
}