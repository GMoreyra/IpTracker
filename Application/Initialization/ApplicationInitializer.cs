using Application.ExternalServiceClients.CountryInfo;
using Application.ExternalServiceClients.CurrencyInfo;
using Application.ExternalServiceClients.Geolocation;
using Application.Interfaces;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using System;

namespace Application.Initialization;

public static class ApplicationInitializer
{
    public static IServiceCollection RegisterApplication(this IServiceCollection services)
    {
        services
            .AddServices()
            .AddRefit();

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ITrackerService, TrackerService>();

        return services;
    }

    private static IServiceCollection AddRefit(this IServiceCollection services)
    {
        services
            .AddRefitClient<IGeoLocationClient>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri("http://ipwhois.app/"));

        services
            .AddRefitClient<ICountryInformationClient>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://api.exchangerate-api.com/v4/latest/USD"));

        services
            .AddRefitClient<ICurrencyInformationClient>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri("http://data.fixer.io/api/"));
     
        return services;
    }
}
