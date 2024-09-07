using Microsoft.OpenApi.Models;
using System.Reflection;

namespace SGONGA.WebAPI.API.Configurations;

public static class SwaggerConfig
{
    public static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);

            c.SwaggerDoc("v1", new OpenApiInfo()
            {
                Title = "Sistema de Gestão para ONGs de Animais — API",
                Version = "V1",
                Description = "Backend Sistema de Gestão para ONGs de Animais.",
                Contact = new OpenApiContact() { Name = "Vinícius Alarcon", Email = "alarcon.vinicius74@gmail.com" },
                License = new OpenApiLicense() { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") }
            });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "Insira o token JWT desta maneira: Bearer {seu token}",
                Name = "Authorization",
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
        });

        return services;
    }

    public static IApplicationBuilder UseSwaggerConfig(this IApplicationBuilder app)
    {
        app.UseSwagger();

        app.UseReDoc(c =>
        {
            c.DocumentTitle = "Sistema de Gestão para ONGs de Animais V1";
            c.RoutePrefix = "redoc";
            c.SpecUrl = "/swagger/v1/swagger.json";
        });
        app.UseSwaggerUI(opt =>
        {
            opt.SwaggerEndpoint("/swagger/v1/swagger.json", "Sistema de Gestão para ONGs de Animais V1");
        });
        return app;
    }
}