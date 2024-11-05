using SGONGA.WebAPI.Business.Tenants.Interfaces.Handlers;

namespace SGONGA.WebAPI.API.Middlewares;

public class TenantIdentifierMiddleware
{
    private readonly RequestDelegate next;

    public TenantIdentifierMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task InvokeAsync(HttpContext context, ITenantProvider tenantProvider)
    {
        tenantProvider.SetTenantId(context.Request.Headers);

        await next(context);
    }
}