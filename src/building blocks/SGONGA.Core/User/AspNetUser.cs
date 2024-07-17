using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace SGONGA.Core.User;

public interface IAspNetUser
{
    string Name { get; }
    Guid GetUserId();
    Guid GetUserIdByJwt();
    Guid GetTenantId();
    string GetUserEmail();
    string GetUserToken();
    bool IsAuthenticated();
    bool HasRole(string role);
    bool HasClaim(string claimType, string claimValue);
    IEnumerable<Claim> GetClaims();
    HttpContext GetHttpContext();
}
public class AspNetUser : IAspNetUser
{
    private readonly IHttpContextAccessor _accessor;

    public AspNetUser(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }

    private HttpContext HttpContext => _accessor.HttpContext;
    public string Name => HttpContext!.User.Identity!.Name!;

    public Guid GetUserId()
    {
        return IsAuthenticated() ? Guid.Parse(HttpContext!.User.GetUserId()) : Guid.Empty;
    }
    public Guid GetUserIdByJwt()
    {
        return IsAuthenticated() ? Guid.Parse(HttpContext!.User.GetUserIdByJwt()) : Guid.Empty;
    }
    public Guid GetTenantId()
    {
        return IsAuthenticated() ? HttpContext!.User.GetTenantId() : Guid.Empty;
    }

    public string GetUserEmail()
    {
        return IsAuthenticated() ? HttpContext!.User.GetUserEmail() : "";
    }

    public string GetUserToken()
    {
        return IsAuthenticated() ? HttpContext!.User.GetUserToken() : "";
    }

    public bool IsAuthenticated()
    {
        return HttpContext?.User?.Identity?.IsAuthenticated ?? false;
    }

    public bool HasRole(string role)
    {
        return IsAuthenticated() ? HttpContext!.User.IsInRole(role) : false;
    }

    public bool HasClaim(string claimType, string claimValue)
    {
        if (!IsAuthenticated()) return false;

        var claim = HttpContext!.User.Claims.FirstOrDefault(c => c.Type == claimType);
        if (claim == null) return false;

        var values = claim.Value.Split(',');
        return values.Contains(claimValue);
    }

    public IEnumerable<Claim> GetClaims()
    {
        return HttpContext!.User.Claims;
    }

    public HttpContext GetHttpContext()
    {
        return HttpContext!;
    }
}
