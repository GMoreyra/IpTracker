using Application.Interfaces;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Initialization;

public static class ApplicationInitializer
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<ITrackerService, TrackerService>();

        return services;
    }
}
