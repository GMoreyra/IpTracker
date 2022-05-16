using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public interface IIpTrackerRepository
    {
        Task<IpToLocationModel> ReturnCountryInfo(string ipNumber);
        Task<List<string>> ReturnMoneyInfo(List<string> currenciesCode);
    }
}