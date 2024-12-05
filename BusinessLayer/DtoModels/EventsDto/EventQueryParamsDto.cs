using BusinessLayer.DtoModels.CommonDto;
using BusinessLayer.DtoModels.EventsDto.QueryParams;

namespace BusinessLayer.DtoModels.EventsDto;

public class EventQueryParamsDto
{
    public EventFiltersDto? Filters { get; set; }
    public PageParamsDto? PageParams { get; set; }
}