using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;

namespace Application.Interfaces;

/// <summary>
/// Interface for the Tracker service.
/// </summary>
public interface ITrackerService
{
    /// <summary>
    /// Returns the ip information based on the ip address
    /// </summary>
    /// <param name="ipAddress">ip address</param>
    /// <returns></returns>
    Task<IpInfoModel> GetIpInfo(string ipAddress);

    /// <summary>
    /// Returns the maximum and minimum distance from Buenos Aires
    /// </summary>
    /// <returns></returns>
    Task<List<StatisticModel>> GetStatistics();

    /// <summary>
    /// Returns the average distance to Buenos Aires between 
    /// the maximum and minimum distance
    /// </summary>
    /// <returns></returns>
    Task<string> GetAverageDistance();
}