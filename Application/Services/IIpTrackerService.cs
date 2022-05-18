using System.Threading.Tasks;
using Domain.Models;

namespace Application.Services
{
    public interface IIpTrackerService
    {

        /// <summary>
        /// Returns the ip information based on the ip number
        /// </summary>
        /// <param name="ipNumber">Ip number</param>
        /// <returns></returns>
        Task<IpInfoModel> GetIpInfo(string ipNumber);
    }
}