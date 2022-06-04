using Domain.Models;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using RestSharp;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utils;

namespace Data.Repositories
{
    public class IpTrackerRepository : IIpTrackerRepository
    {
        private static readonly string _apiKey = "z6MKj5YtvvtUnORpxxvASLIUvzfeo4Aq";
        private static readonly string _urlIpToLocation = "https://api.apilayer.com/ip_to_location/";
        private static readonly string _urlFixer = "https://api.apilayer.com/fixer/";
        private static readonly string _patternKey = KeyUtils.APP + KeyUtils.STATISTIC + "*";

        private readonly IDistributedCache _memoryCache;

        public IpTrackerRepository(IDistributedCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public async Task<IpToLocationModel> ReturnCountryInfo(string ipAddress)
        {
            var ipToLocation = new IpToLocationModel();
            var response = await GetResponseFromAPI(ipAddress, _urlIpToLocation);

            if (response.IsSuccessful)
            {
                ipToLocation = JsonConvert.DeserializeObject<IpToLocationModel>(response.Content);
            }

            return ipToLocation;
        }

        public async Task<List<string>> ReturnMoneyInfo(List<string> currenciesCode)
        {
            var results = new List<string>();

            foreach (var code in currenciesCode)
            {
                var recordKey = KeyUtils.CURRENCY + code;
                var cached = await _memoryCache.GetRecordAsync<string>(recordKey);

                if (cached is null)
                {
                    var parameters = string.Format("convert?to={0}&from={1}&amount={2}", "USD", code, "1");
                    var response = await GetResponseFromAPI(parameters, _urlFixer);

                    if (response.IsSuccessful)
                    {
                        var fixer = JsonConvert.DeserializeObject<FixerModel>(response.Content);
                        results.Add(fixer.Result);

                        await _memoryCache.SetRecordAsync(recordKey, fixer.Result);
                    }
                }
                else
                {
                    results.Add(cached);
                }
            };

            return results;
        }

        public async void AddStatistic(IpInfoModel ipInfoModel)
        {
            if (ipInfoModel is not null 
                && !string.IsNullOrWhiteSpace(ipInfoModel.DistanceToBA) 
                && !string.IsNullOrWhiteSpace(ipInfoModel.Country))
            {
                var statsKey = GetStatiscticKey(ipInfoModel);
                var cached = await _memoryCache.GetRecordAsync<StatisticModel>(statsKey);

                if (cached is null)
                {
                    var statistic = new StatisticModel()
                    {

                        CountryName = ipInfoModel.Country,
                        DistanceToBaKms = StringUtils.StringKmsToInt(ipInfoModel.DistanceToBA)
                    };

                    await _memoryCache.SetRecordAsync(statsKey, statistic, TimeSpan.FromHours(24));
                }
                else
                {
                    await _memoryCache.RemoveAsync(statsKey);

                    cached.InvocationCounter++;

                    await _memoryCache.SetRecordAsync(statsKey, cached, TimeSpan.FromHours(24));
                }
            }
        }

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
            var alltatistics = new List<StatisticModel>();
            using (var redis = ConnectionMultiplexer.Connect("localhost:6379,allowAdmin=true"))
            {
                IDatabase db = redis.GetDatabase(0);
                var keys = redis.GetServer("localhost:6379").Keys(pattern: _patternKey, pageSize: 1000);
                var keysArr = keys.Select(key => (string)key).ToArray();
                foreach (var key in keysArr)
                {
                    var statsKey = key.Replace(KeyUtils.APP, "");
                    var result = await _memoryCache.GetRecordAsync<StatisticModel>(statsKey);
                    alltatistics.Add(result);
                }
            }

            return alltatistics;
        }

        public List<StatisticModel> ReturnMaxMinStatistics(List<StatisticModel> statisticModels)
        {
            if (statisticModels is null || statisticModels.Count == 0)
            {
                return new List<StatisticModel>();
            }

            var minMaxStats = new List<StatisticModel>();
            var maxStatisticModel = MinMaxStatisticModel(statisticModels, "Max");
            var minStatisticModel = MinMaxStatisticModel(statisticModels, "Min");

            minMaxStats.Add(maxStatisticModel);
            minMaxStats.Add(minStatisticModel);

            return minMaxStats;
        }

        private StatisticModel MinMaxStatisticModel(List<StatisticModel> statisticModels, string method)
        {
            var distanceValue = 0;

            if (method.ToUpper() == "MAX")
            {
                distanceValue = statisticModels.Max(d => d.DistanceToBaKms);
            }
            else if (method.ToUpper() == "MIN")
            {
                distanceValue = statisticModels.Min(d => d.DistanceToBaKms);
            }
            
            var invocationValue = statisticModels.Where(x => x.DistanceToBaKms == distanceValue).Max(x => x.InvocationCounter);
            var statisticModel = statisticModels.Where(x => x.DistanceToBaKms == distanceValue && x.InvocationCounter == invocationValue).First();

            return statisticModel;
        }

        private async Task<RestResponse> GetResponseFromAPI(string parameter, string url)
        {
            var client = new RestClient(url);
            var request = new RestRequest(parameter);
            request.AddHeader("apikey", _apiKey);
            var response = await client.ExecuteGetAsync(request);

            return response;
        }
    }
}
