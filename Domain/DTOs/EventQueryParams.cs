using Application.DtoModels.CommonDto;
using Application.DtoModels.EventsDto;

namespace Domain.DTOs;

public class EventQueryParams (EventFilters? filters, PageParams pageParams)
{
    public EventFilters? Filters { get; } = filters;
    public PageParams PageParams { get; } = pageParams;
}