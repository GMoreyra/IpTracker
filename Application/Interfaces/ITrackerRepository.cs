using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces;

public interface ITrackerRepository
{
    /// <summary>
    /// Returns the country information based on the ip address
    /// </summary>
    /// <param name="ipAddress">ip address</param>
    /// <returns></returns>
    Task<IpToLocationModel> ReturnCountryInfo(string ipAddress);

    /// <summary>
    /// Returns the values in dollars of the currencies
    /// </summary>
    /// <param name="currenciesCode">List of currencies codes</param>
    /// <returns></returns>
    Task<List<string>> ReturnMoneyInfo(List<string> currenciesCode);

    /// <summary>
    /// Add the IpInfoModel to the statistics
    /// </summary>
    /// <param name="ipInfoModel"></param>
    void AddStatistic(IpInfoModel ipInfoModel);

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