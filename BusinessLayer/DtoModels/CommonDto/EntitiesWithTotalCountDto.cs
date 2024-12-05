namespace BusinessLayer.DtoModels.CommonDto;

public class EntitiesWithTotalCountDto<T> 
{
    public EntitiesWithTotalCountDto(IEnumerable<T> entities, int totalFields)
    {
        Entities = entities;
        TotalFields = totalFields;
    }
    public int TotalFields { get; set; }
    public IEnumerable<T> Entities { get; set;}
  
}