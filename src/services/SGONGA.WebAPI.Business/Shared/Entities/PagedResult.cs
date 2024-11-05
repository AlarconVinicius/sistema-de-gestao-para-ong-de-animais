namespace SGONGA.WebAPI.Business.Shared.Entities;

public class PagedResult<T> where T : class
{
    public IEnumerable<T> List { get; set; } = Enumerable.Empty<T>();
    public int TotalResults { get; set; }
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public string? Query { get; set; }
    public bool ReturnAll { get; set; }
}