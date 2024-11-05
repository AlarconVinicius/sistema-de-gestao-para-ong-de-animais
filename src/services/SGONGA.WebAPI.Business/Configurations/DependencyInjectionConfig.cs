using Microsoft.Extensions.DependencyInjection;
using SGONGA.WebAPI.Business.Tenants.Handlers;
using SGONGA.WebAPI.Business.Tenants.Interfaces.Handlers;

namespace SGONGA.WebAPI.Business.Configurations;

public static class DependencyInjectionConfig
{
    public static IServiceCollection RegisterBusinessServices(this IServiceCollection services)
    {
        services.AddScoped<ITenantProvider, TenantProvider>();

        return services;
    }
}