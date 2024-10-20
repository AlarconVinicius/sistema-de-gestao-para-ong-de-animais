namespace SGONGA.WebAPI.API.Configurations;

public static class DependencyInjectionConfig
{
    public static IServiceCollection RegisterApiServices(this IServiceCollection services)
    {
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        return services;
    }
}
