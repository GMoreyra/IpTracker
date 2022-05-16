using IpTracker.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace IpTracker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IpTrackerController : ControllerBase
    {
        private readonly IIpTrackerService _service;

        public IpTrackerController(IIpTrackerService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("{ipNumber}")]
        public Task<IpInfo> GetData(string ipNumber)
        {
            var response = _service.GetAllInfoBasedOnSearchCritera(ipNumber);

            return response;
        }
    }
}
