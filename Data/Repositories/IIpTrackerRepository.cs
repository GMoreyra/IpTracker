using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public interface IIpTrackerRepository
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
    }
}