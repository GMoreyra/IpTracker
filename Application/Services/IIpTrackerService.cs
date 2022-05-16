using System.Threading.Tasks;
using Domain.Models;

namespace Application.Services
{
    public interface IIpTrackerService
    {
        Task<IpInfo> GetAllInfoBasedOnSearchCritera(string ipNumber);
    }
}