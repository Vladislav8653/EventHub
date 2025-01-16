namespace Application.DtoModels.CommonDto;

public class EntitiesWithTotalCountDto<T>(IEnumerable<T> entities, int totalFields)
{
    public int TotalFields { get; set; } = totalFields;
    public IEnumerable<T> Entities { get; set;} = entities;
}