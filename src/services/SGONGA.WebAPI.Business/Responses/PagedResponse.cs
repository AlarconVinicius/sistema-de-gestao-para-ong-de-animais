using System.Text.Json.Serialization;

namespace SGONGA.WebAPI.Business.Responses;

public class PagedResponse<TData>
{
    public IEnumerable<TData> List { get; set; } = new List<TData>();
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int TotalResults { get; set; }
    public int TotalPages => (int)Math.Ceiling(TotalResults / (double)PageSize);
    public string? Query { get; set; }
    public bool ReturnAll { get; set; }

    public PagedResponse() { }

    [JsonConstructor]
    public PagedResponse(IEnumerable<TData> list, int totalResults, int pageIndex = 1, int pageSize = 50, string? query = null, bool returnAll = false)
    {
        List = list;
        TotalResults = totalResults;
        PageIndex = pageIndex;
        PageSize = pageSize;
        Query = query;
        ReturnAll = returnAll;
    }

}