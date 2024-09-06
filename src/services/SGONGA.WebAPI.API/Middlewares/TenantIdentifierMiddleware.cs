using Microsoft.Extensions.Primitives;
using SGONGA.Core.Extensions;
using SGONGA.WebAPI.Business.Models;

namespace SGONGA.WebAPI.API.Middlewares;

public class TenantIdentifierMiddleware
{
    private readonly RequestDelegate next;

    public TenantIdentifierMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task InvokeAsync(HttpContext context, TenantProvider tenantProvider)
    {
        StringValues tenantIdValues = context.Request.Headers["TenantId"];

        tenantProvider.TenantId = tenantIdValues.Count > 0
            ? tenantIdValues[0].TryParseGuid()
            : null;

        await next(context);
    }
}