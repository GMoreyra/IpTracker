using Domain.Models;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class IpTrackerRepository : IIpTrackerRepository
    {
        private static readonly string _apiKey = "z6MKj5YtvvtUnORpxxvASLIUvzfeo4Aq";
        private static readonly string _urlIpToLocation = "https://api.apilayer.com/ip_to_location/";
        private static readonly string _urlFixer = "https://api.apilayer.com/fixer/";
        private readonly IMemoryCache _memoryCache;

        public IpTrackerRepository(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public async Task<IpToLocationModel> ReturnCountryInfo(string ipNumber)
        {
            var ipToLocation = new IpToLocationModel();
            var response = await GetResponseFromAPI(ipNumber, _urlIpToLocation);

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
                var cached = _memoryCache.Get<string>(code);

                if (cached is null)
                {
                    var parameters = string.Format("convert?to={0}&from={1}&amount={2}", "USD", code, "1");
                    var response = await GetResponseFromAPI(parameters, _urlFixer);

                    if (response.IsSuccessful)
                    {
                        var fixer = JsonConvert.DeserializeObject<FixerModel>(response.Content);
                        results.Add(fixer.Result);

                        _memoryCache.Set(code, fixer.Result, TimeSpan.FromMinutes(30));
                    }                   
                }
                else
                {
                    results.Add(cached);
                }
            };

            return results;
        }

        private static async Task<RestResponse> GetResponseFromAPI(string parameter, string url)
        {
            var client = new RestClient(url);
            var request = new RestRequest(parameter);
            request.AddHeader("apikey", _apiKey);
            var response = await client.ExecuteGetAsync(request);

            return response;
        }
    }
}
