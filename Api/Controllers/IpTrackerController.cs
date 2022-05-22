using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Utils;

namespace Api.Controllers
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

        /// <summary>
        /// Returns the ip info based on the ip address
        /// </summary>
        /// <param name="ipAddress">ip address</param>
        /// <returns></returns>
        [HttpGet("{ipAddress}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetGeolocation(string ipAddress)
        {
            if (!StringUtils.ValidateString(ipAddress))
            {
                return BadRequest();
            }

            var response = await _service.GetIpInfo(ipAddress);

            if (response is null)
            {
                return NotFound();
            }

            return Ok(response);
        }
    }
}
