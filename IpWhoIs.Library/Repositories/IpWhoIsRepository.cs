using IpWhoIs.Library.Models;
using Newtonsoft.Json;
using RestSharp;
using System.Threading.Tasks;

namespace IpWhoIs.Library.Repositories
{
    public class IpWhoIsRepository : IIpWhoIsRepository
    {
        public async Task<IpWhoIsModel> ReturnCountryInfo(string searchCritera)
        {

            var ipWhoIs = new IpWhoIsModel();
            var client = new RestClient("http://ipwho.is/");
            var request = new RestRequest(searchCritera);
            var response = await client.ExecuteGetAsync(request);

            if (response.IsSuccessful)
            {
                ipWhoIs = JsonConvert.DeserializeObject<IpWhoIsModel>(response.Content);
            }

            return ipWhoIs;
        }
    }
}
