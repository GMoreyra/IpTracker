using Application.ExternalClients.CountryInfo.Models;
using Application.ExternalClients.CurrencyInfo.Models;
using Application.ExternalClients.Geolocation.Models;
using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories;

public interface ITrackerRepository
{
    void SetGeolocationInformation(GeoLocationResponse geoLocation);

    void SetCurrenciesInformation(CurrencyInformationResponse currencyInformation);

    void SetCountriesInformation(IEnumerable<CountryInformationResponse> countryInformationResponses);

     /// <summary>
    /// Returns all invocations
    /// </summary>
    /// <returns></returns>
    Task<List<StatisticModel>> ReturnAllStatistics();

    /// <summary>
    /// Returns the average distance to Buenos Aires
    /// </summary>
    /// <param name="statisticModels"></param>
    /// <returns></returns>
    string ReturnAverageDistanceStatistics(List<StatisticModel> statisticModels);

    /// <summary>
    /// Returns the maximum and minimum distance to Buenos Aires
    /// </summary>
    /// <param name="statisticModels"></param>
    /// <returns></returns>
    List<StatisticModel> ReturnMaxMinStatistics(List<StatisticModel> statisticModels);
}