using IpTracker.Models;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IpTracker.Repositories
{
    public class IpTrackerRepository : IIpTrackerRepository
    {
        public async Task<IpToLocationModel> ReturnCountryInfo(string ipNumber)
        {
            const string url = "https://api.apilayer.com/ip_to_location/";
            var ipToLocation = new IpToLocationModel();
            var response = await GetResponseFromAPI(ipNumber, url);

            if (response.IsSuccessful)
            {
                ipToLocation = JsonConvert.DeserializeObject<IpToLocationModel>(response.Content);
            }

            return ipToLocation;
        }

        public async Task<List<string>> ReturnMoneyInfo(List<string> currenciesCode)
        {
            const string url = "https://api.apilayer.com/fixer/";
            var results = new List<string>();
            currenciesCode.Remove("USD");
            
            foreach (var code in currenciesCode)
            {
                var parameters = string.Format("convert?to={0}&from={1}&amount={2}", "USD", code, "1");
                var response = await GetResponseFromAPI(parameters, url);

                if (response.IsSuccessful)
                {
                    var fixer = JsonConvert.DeserializeObject<FixerModel>(response.Content);
                    results.Add(fixer.Result);
                }
            };

            return results;
        }

        private static async Task<RestResponse> GetResponseFromAPI(string parameter, string url)
        {
            var client = new RestClient(url);
            var request = new RestRequest(parameter);
            request.AddHeader("apikey", "");
            var response = await client.ExecuteGetAsync(request);

            return response;
        }
    }
}
