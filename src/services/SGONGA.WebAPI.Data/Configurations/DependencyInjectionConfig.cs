﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SGONGA.WebAPI.Business.Animals.Interfaces.Repositories;
using SGONGA.WebAPI.Business.People.Interfaces.Repositories;
using SGONGA.WebAPI.Business.Shared.Interfaces.Repositories;
using SGONGA.WebAPI.Data.Context;
using SGONGA.WebAPI.Data.Repositories;

namespace SGONGA.WebAPI.Data.Configurations;

public static class DependencyInjectionConfig
{
    public static IServiceCollection RegisterDataServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<OrganizationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection") ??
                throw new InvalidOperationException("Connection String is not found")));

        services.AddScoped<IGenericUnitOfWork, GenericUnitOfWork>();
        services.AddScoped<IAnimalRepository, AnimalRepository>();
        services.AddScoped<IPersonRepository, PersonRepository>();

        return services;
    }
}
