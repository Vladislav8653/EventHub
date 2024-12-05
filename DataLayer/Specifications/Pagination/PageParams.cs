namespace DataLayer.Specifications.Pagination;

public class PageParams
{
    public PageParams(int? page, int? pageSize, int defaultPage, int defaultPageSize)
    {
        Page = page ?? defaultPage;
        PageSize = pageSize ?? defaultPageSize;
    }

    public int Page { get; }
    public int PageSize { get; }
}