using Application.Interfaces;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Initializer;

public static class ApplicationInitializer
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ITrackerService, TrackerService>();

        return services;
    }
}
