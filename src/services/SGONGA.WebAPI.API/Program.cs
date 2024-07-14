using SGONGA.Core.Configurations;
using SGONGA.WebAPI.API.Configurations;
using SGONGA.WebAPI.Data.Configurations;
using SGONGA.WebAPI.Business.Configurations;
using SGONGA.WebAPI.Identity.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiConfig();

builder.Services.RegisterApiServices()
                .RegisterCoreServices()
                .RegisterDataServices(builder.Configuration)
                .RegisterIdentityServices(builder.Configuration)
                .RegisterBusinessServices();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfig();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerConfig();
}

app.UseApiConfig(app.Environment);

app.CheckAndApplyDatabaseMigrations(app.Services);

app.Run();
