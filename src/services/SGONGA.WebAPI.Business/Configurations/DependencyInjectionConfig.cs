using Microsoft.Extensions.DependencyInjection;
using SGONGA.WebAPI.Business.Handlers;
using SGONGA.WebAPI.Business.Interfaces.Handlers;

namespace SGONGA.WebAPI.Business.Configurations;

public static class DependencyInjectionConfig
{
    public static IServiceCollection RegisterBusinessServices(this IServiceCollection services)
    {
        services.AddScoped<IONGHandler, ONGHandler>();
        services.AddScoped<IAnimalHandler, AnimalHandler>();
        services.AddScoped<IColaboradorHandler, ColaboradorHandler>();

        return services;
    }
}