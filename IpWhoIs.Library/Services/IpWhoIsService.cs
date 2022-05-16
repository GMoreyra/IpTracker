using IpWhoIs.Library.Models;
using IpWhoIs.Library.Repositories;
using System.Threading.Tasks;

namespace IpWhoIs.Library.Services
{
    public class IpWhoIsService : IIpWhoIsService
    {
        private static IIpWhoIsRepository _getWhoIs;

        public IpWhoIsService(IIpWhoIsRepository getWhoIs)
        {
            _getWhoIs = getWhoIs;
        }

        public async Task<IpWhoIsModel> GetCountryInfoBasedOnSearchCritera(string searchCritera)
        {
            return await _getWhoIs.ReturnCountryInfo(searchCritera);
        }
    }
}
