using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SGONGA.WebAPI.Business.Interfaces.Repositories;
using SGONGA.WebAPI.Data.Context;
using SGONGA.WebAPI.Data.Repositories;

namespace SGONGA.WebAPI.Data.Configurations;

public static class DependencyInjectionConfig
{
    public static IServiceCollection RegisterDataServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ONGDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection") ??
                throw new InvalidOperationException("Connection String is not found")));

        services.AddScoped<IONGDbContext, ONGDbContext>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IONGRepository, ONGRepository>();
        services.AddScoped<IAnimalRepository, AnimalRepository>();

        return services;
    }
}
