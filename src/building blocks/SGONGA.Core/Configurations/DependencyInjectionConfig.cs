using Microsoft.Extensions.DependencyInjection;
using SGONGA.Core.Notifications;
using SGONGA.Core.User;

namespace SGONGA.Core.Configurations;

public static class DependencyInjectionConfig
{
    public static IServiceCollection RegisterCoreServices(this IServiceCollection services)
    {
        services.AddScoped<INotifier, Notifier>();
        services.AddScoped<IAspNetUser, AspNetUser>();

        return services;
    }
}