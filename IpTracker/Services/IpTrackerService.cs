using AutoMapper;
using IpTracker.Repositories;
using System.Threading.Tasks;

namespace IpTracker.Services
{
    public class IpTrackerService : IIpTrackerService
    {
        private static IIpTrackerRepository _getWhoIs;
        private static IMapper _mapper;

        public IpTrackerService(IIpTrackerRepository getWhoIs, IMapper mapper)
        {
            _getWhoIs = getWhoIs;
            _mapper = mapper;
        }

        public async Task<IpInfo> GetAllInfoBasedOnSearchCritera(string ipNumber)
        {
            var ipWhoIsData = await _getWhoIs.ReturnCountryInfo(ipNumber);
            ipWhoIsData.CurrenciesDollarValue = await _getWhoIs.ReturnMoneyInfo(ipWhoIsData.CurrenciesList);

            return _mapper.Map<IpInfo>(ipWhoIsData);
        }
    }
}
