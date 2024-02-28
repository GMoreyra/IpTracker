namespace Application.Initialization;

using Application.ExternalServiceClients.CountryInfo;
using Application.ExternalServiceClients.CurrencyInfo;
using Application.ExternalServiceClients.Geolocation;
using Application.Interfaces.Services;
using Application.Options;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using System;

/// <summary>
/// Class responsible for initializing the application services and external service clients.
/// </summary>
public static class ApplicationInitializer
{
    /// <summary>
    /// Registers the application services and external service clients.
    /// </summary>
    /// <param name="services">The service collection to add the services to.</param>
    /// <param name="externalServiceOptions">The options for the external service clients.</param>
    /// <returns>The service collection with the registered services.</returns>
    public static IServiceCollection RegisterApplication(this IServiceCollection services, ExternalServiceOptions externalServiceOptions)
    {
        services
            .AddServices()
            .AddRefit(externalServiceOptions);

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ITrackerService, TrackerService>();

        return services;
    }

    private static IServiceCollection AddRefit(this IServiceCollection services, ExternalServiceOptions externalServiceOptions)
    {
        services
            .AddRefitClient<IGeoLocationClient>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(externalServiceOptions.GeoLocationClient));

        services
            .AddRefitClient<ICountryInformationClient>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(externalServiceOptions.CountryClient));

        services
            .AddRefitClient<ICurrencyInformationClient>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(externalServiceOptions.CurrencyClient));

        return services;
    }
}
