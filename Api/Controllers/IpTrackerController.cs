using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Utils;

namespace Api.Controllers;

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
    public async Task<IActionResult> GetGeolocationAsync(string ipAddress)
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

    /// <summary>
    /// Returns the statistics
    /// </summary>
    [HttpGet("statistic")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetStatisticsAsync()
    {
        var response = await _service.GetStatistics();

        if (response is null)
        {
            return NotFound();
        }

        return Ok(response);
    }

    /// <summary>
    /// Returns the average distance
    /// </summary>
    [HttpGet("average")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAverageDistanceAsync()
    {
        var response = await _service.GetAverageDistance();

        if (response is null)
        {
            return NotFound();
        }

        return Ok(response);
    }
}
