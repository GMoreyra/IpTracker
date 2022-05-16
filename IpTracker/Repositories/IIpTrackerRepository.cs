using IpTracker.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IpTracker.Repositories
{
    public interface IIpTrackerRepository
    {
        Task<IpToLocationModel> ReturnCountryInfo(string searchCritera);
        Task<List<string>> ReturnMoneyInfo(List<string> currenciesCode);
    }
}