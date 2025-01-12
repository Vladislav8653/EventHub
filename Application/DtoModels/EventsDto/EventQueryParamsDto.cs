using Application.DtoModels.CommonDto;
using Application.DtoModels.EventsDto.QueryParams;

namespace Application.DtoModels.EventsDto;

public class EventQueryParamsDto
{
    public EventFiltersDto? Filters { get; set; }
    public PageParamsDto? PageParams { get; set; }
}