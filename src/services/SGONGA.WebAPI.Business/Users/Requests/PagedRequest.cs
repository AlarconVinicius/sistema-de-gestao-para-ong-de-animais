namespace SGONGA.WebAPI.Business.Users.Requests;

public abstract class PagedRequest : Request
{
    public int PageSize { get; set; } = 50;
    public int PageNumber { get; set; } = 1;
    public string? Query { get; set; } = null;
    public bool ReturnAll { get; set; }

    public PagedRequest() { }

    protected PagedRequest(int pageSize, int pageNumber, string? query, bool returnAll)
    {
        PageSize = pageSize;
        PageNumber = pageNumber;
        Query = query;
        ReturnAll = returnAll;
    }
}