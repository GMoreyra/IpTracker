using IpWhoIs.Library.Models;
using System.Threading.Tasks;

namespace IpWhoIs.Library.Services
{
    public interface IIpWhoIsService
    {
        Task<IpWhoIsModel> GetCountryInfoBasedOnSearchCritera(string searchCritera);
    }
}