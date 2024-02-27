using Application.ExternalServiceClients.CountryInfo;
using Application.ExternalServiceClients.CurrencyInfo;
using Application.ExternalServiceClients.Geolocation;
using Application.Interfaces;
using Application.Options;
using Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Refit;
using System;

namespace Application.Initialization;

public static class ApplicationInitializer
{
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
