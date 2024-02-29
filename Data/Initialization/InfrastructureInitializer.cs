using Application.IGateways;
using Application.Interfaces.Repositories;
using Infrastructure.Gateways.CountryInfo;
using Infrastructure.Gateways.CurrencyInfo;
using Infrastructure.Gateways.Geolocation;
using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Utils;

namespace Infrastructure.Initialization;

public static class InfrastructureInitializer
{
    public static IServiceCollection RegisterInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddRespositories()
            .AddGateways()
            .AddRedis(configuration);

        return services;
    }

    private static IServiceCollection AddRespositories(this IServiceCollection services)
    {
        services.AddScoped<ITrackerRepository, TrackerRepository>();

        return services;
    }

    private static IServiceCollection AddGateways(this IServiceCollection services)
    {
        services.AddScoped<ICountryInfoGateway, CountryInfoGateway>();
        services.AddScoped<ICurrencyInfoGateway, CurrencyInfoGateway>();
        services.AddScoped<IGeolocationGateway, GeolocationGateway>();

        return services;
    }

    private static IServiceCollection AddRedis(this IServiceCollection services, IConfiguration config)
    {
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = config.GetConnectionString("Redis");
            options.InstanceName = KeyUtils.APP;
        });

        return services;
    }
}
