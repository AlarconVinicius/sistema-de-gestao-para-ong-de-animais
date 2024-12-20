﻿using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SGONGA.WebAPI.API.Infrastructure;
using SGONGA.WebAPI.API.Middlewares;
using SGONGA.WebAPI.Data.Context;
using SGONGA.WebAPI.Data.Seed;
using SGONGA.WebAPI.Identity.Context;
using SGONGA.WebAPI.Identity.Seed;
using System.Globalization;

namespace SGONGA.WebAPI.API.Configurations;
public static class ApiConfig
{
    public static IServiceCollection AddApiConfig(this IServiceCollection services)
    {
        CultureInfo.CurrentCulture = new CultureInfo("pt-BR");
        FluentValidation.ValidatorOptions.Global.LanguageManager.Culture = CultureInfo.CurrentCulture;
        FluentValidation.ValidatorOptions.Global.DefaultRuleLevelCascadeMode = FluentValidation.CascadeMode.Stop;
        services.AddControllers();

        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;

        });

        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();

        services.AddCrossOrigin();

        return services;
    }

    public static IApplicationBuilder UseApiConfig(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseCors(ApiConfigurationDefault.CorsPolicyName);
        }
        else
        {
            app.UseCors(ApiConfigurationDefault.CorsPolicyName);
            app.UseHsts();
        }

        app.UseHttpsRedirection(); 

        app.UseMiddleware<TenantIdentifierMiddleware>();

        app.UseExceptionHandler();

        app.UseRouting();

        var supportedCultures = new[] { new CultureInfo("pt-BR") };
        app.UseRequestLocalization(new RequestLocalizationOptions
        {
            DefaultRequestCulture = new RequestCulture("pt-BR"),
            SupportedCultures = supportedCultures,
            SupportedUICultures = supportedCultures
        });

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapGet("/", async context =>
            {
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync("{\"Ok\": true, \"PathSwagger\": \"/swagger/index.html\"}");
            });

        });
        return app;
    }

    public static void CheckAndApplyDatabaseMigrations(this IApplicationBuilder app, IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var identityDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var emsDbContext = scope.ServiceProvider.GetRequiredService<OrganizationDbContext>();
        if (identityDbContext.Database.GetPendingMigrations().Any())
        {
            identityDbContext.Database.Migrate();
        }
        if (emsDbContext.Database.GetPendingMigrations().Any())
        {
            emsDbContext.Database.Migrate();
        }
    }

    public static void RunSeeds(this IApplicationBuilder app, IServiceProvider serviceProvider)
    {
        SeedONGData.SeedUsuarioDefault(serviceProvider);

        SeedIdentityDataAsync.SeedIdentityAsync(serviceProvider).GetAwaiter().GetResult();
    }


    private static IServiceCollection AddCrossOrigin(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(ApiConfigurationDefault.CorsPolicyName,
                policy =>
                    policy
                        //.WithOrigins(ConfigurationDefault.ApiUrl, ConfigurationDefault.SiteUrl, ConfigurationDefault.PanelUrl)
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        //.AllowCredentials()
                        );
        });
        return services;
    }
}