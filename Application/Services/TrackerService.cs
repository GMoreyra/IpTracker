using Application.ExternalClients.CountryInfo.Models;
using Application.ExternalClients.CurrencyInfo.Models;
using Application.ExternalClients.Geolocation.Models;
using Application.Interfaces.IGateways;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services;

public class TrackerService : ITrackerService
{
    private readonly ITrackerRepository _trackerRepository;
    private readonly IGeolocationGateway _geolocationGateway;
    private readonly ICountryInfoGateway _countryInfoGateway;
    private readonly ICurrencyInfoGateway _currencyInfoGateway;

    public TrackerService(ITrackerRepository trackerRepository,
                          IGeolocationGateway geolocationGateway,
                          ICountryInfoGateway countryInfoGateway,
                          ICurrencyInfoGateway currencyInfoGateway)
    {
        _trackerRepository = trackerRepository ?? throw new ArgumentNullException(nameof(trackerRepository));
        _countryInfoGateway = countryInfoGateway ?? throw new ArgumentNullException(nameof(countryInfoGateway));
        _geolocationGateway = geolocationGateway ?? throw new ArgumentNullException(nameof(geolocationGateway));
        _currencyInfoGateway = currencyInfoGateway ?? throw new ArgumentNullException(nameof(currencyInfoGateway));
    }

    public async Task<IpInfoModel> GetIpInformation(string ipAddress)
    {
        var geoLocationResponse = await GeoLocationResponse(ipAddress);

        var currencyInformationResponse = await CurrencyInformationResponse();

        var countryInformationResponses = await CountryInformationResponses();

        return new();
    }

    private async Task<GeoLocationResponse> GeoLocationResponse(string ipAddress)
    {
        var geoLocationResponse = await _geolocationGateway.GetGeolocationInfo(ipAddress);

        _trackerRepository.SetGeolocationInformation(geoLocationResponse);

        return geoLocationResponse;
    }

    private async Task<CurrencyInformationResponse> CurrencyInformationResponse()
    {
        var currencyInformation = await _currencyInfoGateway.GetCurrencyInfo();

        _trackerRepository.SetCurrenciesInformation(currencyInformation);

        return currencyInformation;
    }

    private async Task<IEnumerable<CountryInformationResponse>> CountryInformationResponses()
    {
        var countryInformation = await _countryInfoGateway.GetCountryInfo();

        _trackerRepository.SetCountriesInformation(countryInformation);

        return countryInformation;
    }

    //private void SetStatistic(IpInfoModel ipInfoModel)
    //{
    //    _trackerRepository.AddStatistic(ipInfoModel);
    //}

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
