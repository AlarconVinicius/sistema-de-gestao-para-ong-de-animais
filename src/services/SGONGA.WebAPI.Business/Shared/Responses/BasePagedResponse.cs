using System.Text.Json.Serialization;

namespace SGONGA.WebAPI.Business.Shared.Responses;

public record BasePagedResponse<TData>(int PageIndex, int PageSize, int TotalResults, string? Query, bool ReturnAll) : BaseResponse
{
    public IEnumerable<TData> List { get; set; } = new List<TData>();

    public int TotalPages => (int)Math.Ceiling(TotalResults / (double)PageSize);

    public BasePagedResponse() : this(default, default, default, default, default) { }

    [JsonConstructor]
    public BasePagedResponse(IEnumerable<TData> list, int totalResults, int pageIndex = 1, int pageSize = 50, string? query = null, bool returnAll = false) : this(pageIndex, pageSize, totalResults, query, returnAll)
    {
        List = list;
    }

}