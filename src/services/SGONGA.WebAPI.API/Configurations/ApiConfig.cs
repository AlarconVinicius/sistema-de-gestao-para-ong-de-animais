using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SGONGA.Core.Configurations;
using SGONGA.WebAPI.Data.Context;
using SGONGA.WebAPI.Identity.Context;

namespace SGONGA.WebAPI.API.Configurations;
public static class ApiConfig
{
    public static IServiceCollection AddApiConfig(this IServiceCollection services)
    {
        services.AddControllers();

        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;

        });

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

        app.UseRouting();

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
        var emsDbContext = scope.ServiceProvider.GetRequiredService<ONGDbContext>();
        if (identityDbContext.Database.GetPendingMigrations().Any())
        {
            identityDbContext.Database.Migrate();
        }
        if (emsDbContext.Database.GetPendingMigrations().Any())
        {
            emsDbContext.Database.Migrate();
        }
    }


    private static IServiceCollection AddCrossOrigin(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(ApiConfigurationDefault.CorsPolicyName,
                policy =>
                    policy
                        .WithOrigins(ConfigurationDefault.ApiUrl, ConfigurationDefault.SiteUrl, ConfigurationDefault.PanelUrl)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
        });
        return services;
    }
}