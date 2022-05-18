using AutoMapper;
using Data.Repositories;
using System.Threading.Tasks;
using Domain.Models;

namespace Application.Services
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

        public async Task<IpInfoModel> GetIpInfo(string ipNumber)
        {
            var ipWhoIsData = await _getWhoIs.ReturnCountryInfo(ipNumber);
            var currencyList = ipWhoIsData.GetCurrenciesList();
            ipWhoIsData.CurrenciesDollarValue = await _getWhoIs.ReturnMoneyInfo(currencyList);

            return _mapper.Map<IpInfoModel>(ipWhoIsData);
        }
    }
}
