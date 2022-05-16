using System.Threading.Tasks;

namespace IpTracker.Services
{
    public interface IIpTrackerService
    {
        Task<IpInfo> GetAllInfoBasedOnSearchCritera(string ipNumber);
    }
}