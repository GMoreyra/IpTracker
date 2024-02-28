using Application.IGateways;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utils;

namespace Application.Services;

public class TrackerService : ITrackerService
{
    private readonly ITrackerRepository _trackerRepository;
    private readonly IMapper _mapper;
    private readonly IGeolocationGateway _geolocationGateway;
    private readonly ICountryInfoGateway _countryInfoGateway;
    private readonly ICurrencyInfoGateway _currencyInfoGateway;

    public TrackerService(ITrackerRepository trackerRepository,
                          IMapper mapper,
                          IGeolocationGateway geolocationGateway,
                          ICountryInfoGateway countryInfoGateway,
                          ICurrencyInfoGateway currencyInfoGateway)
    {
        _trackerRepository = trackerRepository ?? throw new ArgumentNullException(nameof(trackerRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _countryInfoGateway = countryInfoGateway ?? throw new ArgumentNullException(nameof(countryInfoGateway));
        _geolocationGateway = geolocationGateway ?? throw new ArgumentNullException(nameof(geolocationGateway));
        _currencyInfoGateway = currencyInfoGateway ?? throw new ArgumentNullException(nameof(currencyInfoGateway));
    }

    public async Task<IpInfoModel> GetIpInformation(string ipAddress)
    {
        IpInfoModel ipInfoModel;
        var recordKey = KeyUtils.ADDRESS + ipAddress;
        var cached = await _memoryCache.GetRecordAsync<IpInfoModel>(recordKey);

        if (cached is null)
        {
            var ipWhoIsData = await _trackerRepository.ReturnCountryInfo(ipAddress);

            var currencyList = ipWhoIsData.GetCurrenciesList();

            ipWhoIsData.CurrenciesDollarValue = await _trackerRepository.ReturnMoneyInfo(currencyList);

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
        _trackerRepository.AddStatistic(ipInfoModel);
    }

    public async Task<List<StatisticModel>> GetStatistics()
    {
        var allStatistics = await _trackerRepository.ReturnAllStatistics();

        return _trackerRepository.ReturnMaxMinStatistics(allStatistics);
    }

    public async Task<string> GetAverageDistance()
    {
        var allStatistics = await _trackerRepository.ReturnAllStatistics();

        var maxMinStatistics = _trackerRepository.ReturnMaxMinStatistics(allStatistics);

        return _trackerRepository.ReturnAverageDistanceStatistics(maxMinStatistics);
    }
}
