using System.Text.Json.Serialization;

namespace SGONGA.WebAPI.API.Shared;

public class BasePagedResponse<TData>
{
    public IEnumerable<TData> List { get; set; } = new List<TData>();
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int TotalResults { get; set; }
    public int TotalPages => (int)Math.Ceiling(TotalResults / (double)PageSize);
    public string? Query { get; set; }
    public bool ReturnAll { get; set; }

    public BasePagedResponse() { }

    [JsonConstructor]
    public BasePagedResponse(IEnumerable<TData> list, int totalResults, int pageIndex = 1, int pageSize = 50, string? query = null, bool returnAll = false)
    {
        List = list;
        TotalResults = totalResults;
        PageIndex = pageIndex;
        PageSize = pageSize;
        Query = query;
        ReturnAll = returnAll;
    }

}