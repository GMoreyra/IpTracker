using Domain.Models;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utils;

namespace Data.Repositories
{
    public class IpTrackerRepository : IIpTrackerRepository
    {
        private static readonly string _apiKey = "z6MKj5YtvvtUnORpxxvASLIUvzfeo4Aq";
        private static readonly string _urlIpToLocation = "https://api.apilayer.com/ip_to_location/";
        private static readonly string _urlFixer = "https://api.apilayer.com/fixer/";
        private static readonly string _currencykey = "Currency_";
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
                var recordKey = _currencykey + code;
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
