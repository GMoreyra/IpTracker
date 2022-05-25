using AutoMapper;
using Data.Repositories;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.Extensions.Caching.Distributed;
using Utils;
using System;

namespace Application.Services
{
    public class IpTrackerService : IIpTrackerService
    {
        private static IIpTrackerRepository _getWhoIs;
        private static IMapper _mapper;
        private readonly IDistributedCache _memoryCache;
        private static readonly string _adresskey = "IpAdress_";

        public IpTrackerService(IIpTrackerRepository getWhoIs, IMapper mapper, IDistributedCache memoryCache)
        {
            _getWhoIs = getWhoIs;
            _mapper = mapper;
            _memoryCache = memoryCache;
        }

        public async Task<IpInfoModel> GetIpInfo(string ipAddress)
        {
            var recordKey = _adresskey + ipAddress;
            var cached = await _memoryCache.GetRecordAsync<IpInfoModel>(recordKey);

            if (cached is null)
            {
                var ipWhoIsData = await _getWhoIs.ReturnCountryInfo(ipAddress);

                var currencyList = ipWhoIsData.GetCurrenciesList();

                ipWhoIsData.CurrenciesDollarValue = await _getWhoIs.ReturnMoneyInfo(currencyList);

                var ipInfo = _mapper.Map<IpInfoModel>(ipWhoIsData);

                await _memoryCache.SetRecordAsync(_adresskey + ipAddress, ipInfo, TimeSpan.FromHours(12));

                return ipInfo;
            }
            else
            {
                return cached;
            }
        }
    }
}
