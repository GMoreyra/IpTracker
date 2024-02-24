using Application.Interfaces;
using AutoMapper;
using Data.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utils;

namespace Application.Services
{
    public class IpTrackerService : IIpTrackerService
    {
        private static IIpTrackerRepository _getWhoIsRepository;
        private static IMapper _mapper;
        private readonly IDistributedCache _memoryCache;

        public IpTrackerService(IIpTrackerRepository getWhoIs, IMapper mapper, IDistributedCache memoryCache)
        {
            _getWhoIsRepository = getWhoIs;
            _mapper = mapper;
            _memoryCache = memoryCache;
        }

        public async Task<IpInfoModel> GetIpInfo(string ipAddress)
        {
            IpInfoModel ipInfoModel;
            var recordKey = KeyUtils.ADDRESS + ipAddress;
            var cached = await _memoryCache.GetRecordAsync<IpInfoModel>(recordKey);

            if (cached is null)
            {
                var ipWhoIsData = await _getWhoIsRepository.ReturnCountryInfo(ipAddress);

                var currencyList = ipWhoIsData.GetCurrenciesList();

                ipWhoIsData.CurrenciesDollarValue = await _getWhoIsRepository.ReturnMoneyInfo(currencyList);

                var ipInfo = _mapper.Map<IpInfoModel>(ipWhoIsData);

                await _memoryCache.SetRecordAsync(KeyUtils.ADDRESS + ipAddress, ipInfo);

                ipInfoModel = ipInfo;
            }
            else
            {
                ipInfoModel = cached;
            }

            SetStatistic(ipInfoModel);

            return ipInfoModel;
        }

        private void SetStatistic(IpInfoModel ipInfoModel)
        {
            _getWhoIsRepository.AddStatistic(ipInfoModel);
        }

        public async Task<List<StatisticModel>> GetStatistics()
        {
            var allStatistics = await _getWhoIsRepository.ReturnAllStatistics();

            return _getWhoIsRepository.ReturnMaxMinStatistics(allStatistics);
        }

        public async Task<string> GetAverageDistance()
        {
            var allStatistics = await _getWhoIsRepository.ReturnAllStatistics();

            var maxMinStatistics = _getWhoIsRepository.ReturnMaxMinStatistics(allStatistics);

            return _getWhoIsRepository.ReturnAverageDistanceStatistics(maxMinStatistics);
        }
    }
}
