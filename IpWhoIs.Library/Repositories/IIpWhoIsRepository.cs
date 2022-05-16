using IpWhoIs.Library.Models;
using System.Threading.Tasks;

namespace IpWhoIs.Library.Repositories
{
    public interface IIpWhoIsRepository
    {
        Task<IpWhoIsModel> ReturnCountryInfo(string searchCritera);
    }
}