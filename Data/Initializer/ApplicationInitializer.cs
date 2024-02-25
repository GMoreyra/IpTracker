using AutoMapper;
using Data.Interfaces;
using Data.Repositories;
using Mapping.Profiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Utils;

namespace Data.Initializer;

public static class ApplicationInitializer
{
    public static IServiceCollection ConfigureApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRespositories();
        services.AddMapper();
        services.AddRedis(configuration);

        return services;
    }

    private static IServiceCollection AddRespositories(this IServiceCollection services)
    {
        services.AddScoped<ITrackerRepository, TrackerRepository>();

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

    private static IServiceCollection AddMapper(this IServiceCollection services)
    {
        var mapperConfig = new MapperConfiguration(m =>
        {
            m.AddProfile(new IpLocationToIpInfoProfile());
        });

        IMapper mapper = mapperConfig.CreateMapper();
        services.AddSingleton(mapper);

        return services;
    }
}
