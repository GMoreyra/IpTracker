using System.Threading.Tasks;
using Domain.Models;

namespace Api.Services
{
    public interface IIpTrackerService
    {
        Task<IpInfo> GetAllInfoBasedOnSearchCritera(string ipNumber);
    }
}