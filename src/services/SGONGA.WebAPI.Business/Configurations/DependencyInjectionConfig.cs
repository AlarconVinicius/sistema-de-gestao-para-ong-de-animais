using Microsoft.Extensions.DependencyInjection;
using SGONGA.WebAPI.Business.Handlers;
using SGONGA.WebAPI.Business.Interfaces.Handlers;
using SGONGA.WebAPI.Business.Interfaces.Services;
using SGONGA.WebAPI.Business.Models;

namespace SGONGA.WebAPI.Business.Configurations;

public static class DependencyInjectionConfig
{
    public static IServiceCollection RegisterBusinessServices(this IServiceCollection services)
    {
        services.AddScoped<IUsuarioHandler, UsuarioHandler>();
        services.AddScoped<IONGHandler, ONGHandler>();
        services.AddScoped<IAdotanteHandler, AdotanteHandler>();
        services.AddScoped<ITenantProvider, TenantProvider>();

        return services;
    }
}