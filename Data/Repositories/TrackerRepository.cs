using Application.ExternalClients.CountryInfo.Models;
using Application.ExternalClients.CurrencyInfo.Models;
using Application.ExternalClients.Geolocation.Models;
using Application.Interfaces.Repositories;
using Domain.Models;
using NRedisStack;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utils;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Infrastructure.Repositories;

public class TrackerRepository : ITrackerRepository
{
    private static readonly string _apiKey = "z6MKj5YtvvtUnORpxxvASLIUvzfeo4Aq";
    private static readonly string _patternKey = KeyUtils.APP + KeyUtils.STATISTIC + "*";

    public void SetGeolocationInformation(GeoLocationResponse geoLocation)
    {
        using var redis = ConnectionMultiplexer.Connect("localhost");
        IDatabase db = redis.GetDatabase();

        var value = db.StringGet(KeyUtils.APP + "geolocation_" + geoLocation.Ip);

        var geoLocationSerialized = JsonSerializer.Serialize(geoLocation);

        if (!value.HasValue)
        {
            db.StringSet(KeyUtils.APP + "geolocation_", geoLocationSerialized);
        }
    }

    public void SetCurrenciesInformation(CurrencyInformationResponse currencyInformation)
    {
        using var redis = ConnectionMultiplexer.Connect("localhost");
        IDatabase db = redis.GetDatabase();

        var value = db.StringGet(KeyUtils.APP + "currencies");

        var currencySerialized = JsonSerializer.Serialize(currencyInformation);

        if (!value.HasValue)
        {
            db.StringSet(KeyUtils.APP + "currencies", currencySerialized);
        }
    }

    public void SetCountriesInformation(IEnumerable<CountryInformationResponse> countryInformationResponses)
    {
        using var redis = ConnectionMultiplexer.Connect("localhost");
        IDatabase db = redis.GetDatabase();

        var value = db.StringGet(KeyUtils.APP + "countries");
        var currencySerialized = JsonSerializer.Serialize(countryInformationResponses);

        if (!value.HasValue)
        {
            db.StringSet(KeyUtils.APP + "countries", currencySerialized);
        }
    }

    //public async void AddStatistic(IpInfoModel ipInfoModel)
    //{
    //    if (ipInfoModel is not null
    //        && !string.IsNullOrWhiteSpace(ipInfoModel.DistanceToBA)
    //        && !string.IsNullOrWhiteSpace(ipInfoModel.Country))
    //    {
    //        var statsKey = GetStatiscticKey(ipInfoModel);
    //        var cached = await _memoryCache.GetRecordAsync<StatisticModel>(statsKey);

    //        if (cached is null)
    //        {
    //            var statistic = new StatisticModel()
    //            {
    //                CountryName = ipInfoModel.Country,
    //                DistanceToBaInKms = StringUtils.StringKmsToInt(ipInfoModel.DistanceToBA)
    //            };

    //            await _memoryCache.SetRecordAsync(statsKey, statistic, TimeSpan.FromHours(1));
    //        }
    //        else
    //        {
    //            await _memoryCache.RemoveAsync(statsKey);

    //            cached.InvocationCounter++;

    //            await _memoryCache.SetRecordAsync(statsKey, cached, TimeSpan.FromHours(1));
    //        }
    //    }
    //}

    private string GetStatiscticKey(IpInfoModel ipInfoModel)
    {
        var countryName = ipInfoModel.Country;
        var distanceToBa = ipInfoModel.DistanceToBA;
        var distanceParsed = StringUtils.StringKmsToInt(distanceToBa);
        var statsKey = KeyUtils.STATISTIC + countryName + distanceParsed.ToString();

        return statsKey;
    }

    public async Task<List<StatisticModel>> ReturnAllStatistics()
    {
        var allStatistics = new List<StatisticModel>();
        await using var redis = ConnectionMultiplexer.Connect("localhost");
        IDatabase db = redis.GetDatabase();

        var keys = redis.GetServer("redis_image:6379").Keys(pattern: _patternKey, pageSize: 1000);
        var keysArr = keys.Select(key => (string)key).ToArray();
        foreach (var key in keysArr)
        {
            var statsKey = key.Replace(KeyUtils.APP, "");
            var result = await db.StringGetAsync(statsKey);

            allStatistics.Add(JsonSerializer.Deserialize<StatisticModel>(result));
        }

        return allStatistics;
    }

    public string ReturnAverageDistanceStatistics(List<StatisticModel> statisticModels)
    {
        if (statisticModels is null || statisticModels.Count == 0)
        {
            return string.Empty;
        }

        if (statisticModels.Count == 2)
        {
            var averageDistance = (statisticModels[0].InvocationCounter * statisticModels[0].DistanceToBaInKms +
                statisticModels[1].InvocationCounter * statisticModels[1].DistanceToBaInKms) /
                (statisticModels[0].InvocationCounter + statisticModels[1].InvocationCounter);

            return $"{averageDistance} KMs";
        }
        else
        {
            return $"{statisticModels[0].DistanceToBaInKms} KMs";
        }
    }

    public List<StatisticModel> ReturnMaxMinStatistics(List<StatisticModel> statisticModels)
    {
        if (statisticModels is null || statisticModels.Count == 0)
        {
            return [];
        }

        List<StatisticModel> minMaxStats = [];
        var maxStatisticModel = MinMaxStatisticModel(statisticModels, "Max");
        var minStatisticModel = MinMaxStatisticModel(statisticModels, "Min");

        minMaxStats.Add(maxStatisticModel);
        minMaxStats.Add(minStatisticModel);

        return minMaxStats;
    }

    private static StatisticModel MinMaxStatisticModel(List<StatisticModel> statisticModels, string method)
    {
        var distanceValue = 0;

        if (method.Equals("MAX", StringComparison.CurrentCultureIgnoreCase))
        {
            distanceValue = statisticModels.Max(d => d.DistanceToBaInKms);
        }
        else if (method.Equals("MIN", StringComparison.CurrentCultureIgnoreCase))
        {
            distanceValue = statisticModels.Min(d => d.DistanceToBaInKms);
        }

        var invocationValue = statisticModels.Where(x => x.DistanceToBaInKms == distanceValue).Max(x => x.InvocationCounter);
        var statisticModel = statisticModels.Where(x => x.DistanceToBaInKms == distanceValue && x.InvocationCounter == invocationValue).First();

        return statisticModel;
    }
}
