using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;

namespace Application.Services
{
    public interface IIpTrackerService
    {

        /// <summary>
        /// Returns the ip information based on the ip address
        /// </summary>
        /// <param name="ipAddress">ip address</param>
        /// <returns></returns>
        Task<IpInfoModel> GetIpInfo(string ipAddress);

        Task<List<StatisticModel>> GetStatistics();
    }
}