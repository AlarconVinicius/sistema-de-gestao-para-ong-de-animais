using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
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
                Contact = new OpenApiContact() { Name = "Vinícius Alarcon", Email = "alarcon.vinicius74@gmail.com", Url = new Uri("https://www.linkedin.com/in/vinicius-alarcon/") },
                License = new OpenApiLicense() { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") },
                Extensions = new Dictionary<string, IOpenApiExtension>
                {
                    {
                        "x-logo", new OpenApiObject
                        {
                            {"url", new OpenApiString("https://iili.io/d8jKOJe.png")},
                            { "altText", new OpenApiString("Sistema de Gestão para ONGs de Animais")}
                        }
                    }
                }
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
            //c.ExampleFilters();
            c.OperationFilter<TenantHeaderParameter>();
        });

        //services.AddSwaggerExamplesFromAssemblyOf<TenantHeaderParameter>();
        //services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());

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
public class TenantHeaderParameter : IOperationFilter
{
    //private static readonly string[] operationWithoutHeader = ["Admin"];

    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.Parameters == null)
        {
            operation.Parameters = [];
        }
        if (context.ApiDescription.RelativePath!.Contains("admin", StringComparison.OrdinalIgnoreCase))
        {
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "TenantId",
                In = ParameterLocation.Header,
                Description = "Identificador do locatário que está fazendo a requisição.",
                Required = true
            });
        }
        //if (!operation.Tags.Any(x => operationWithoutHeader.Contains(x.Name)))
        //{
        //    operation.Parameters.Add(new OpenApiParameter
        //    {
        //        Name = "TenantId",
        //        In = ParameterLocation.Header,
        //        Description = "Id do tenant que o usuário vai se conectar",
        //        Required = true
        //    });
        //}
    }
}