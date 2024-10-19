using SGONGA.WebAPI.API.Abstractions.Messaging;

namespace SGONGA.WebAPI.API.Shared;

public class BasePagedQuery<TResponse> : IQuery<TResponse>
{
    public int PageSize { get; set; } = 50;
    public int PageNumber { get; set; } = 1;
    public string? Query { get; set; } = null;
    public bool ReturnAll { get; set; }

    public BasePagedQuery() { }

    protected BasePagedQuery(int pageSize, int pageNumber, string? query, bool returnAll)
    {
        PageSize = pageSize;
        PageNumber = pageNumber;
        Query = query;
        ReturnAll = returnAll;
    }
}