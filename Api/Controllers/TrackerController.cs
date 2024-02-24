using Application.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Utils;

using Media = System.Net.Mime.MediaTypeNames;

namespace Api.Controllers;

/// <summary>
/// Controller for tracking IP addresses.
/// </summary>
[ApiController]
[Route("[controller]")]
[Produces(Media.Application.Json)]
[Consumes(Media.Application.Json)]
public class TrackerController : ControllerBase
{
    private readonly ITrackerService _service;

    /// <summary>
    /// Initializes a new instance of the <see cref="TrackerController"/> class.
    /// </summary>
    /// <param name="service">The service to be used for IP tracking.</param>
    /// <exception cref="ArgumentNullException">Thrown when the provided service is null.</exception>
    public TrackerController(ITrackerService service)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }

    /// <summary>
    /// Gets the geolocation of the specified IP address.
    /// </summary>
    /// <param name="ipAddress">The IP address to get the geolocation for.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the action result.</returns>
    [HttpGet("{ipAddress}")]
    [ProducesResponseType(typeof(List<StatisticModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IpInfoModel>> GetGeolocationAsync(string ipAddress)
    {
        if (!StringUtils.ValidateString(ipAddress))
        {
            return BadRequest(ipAddress);
        }

        var response = await _service.GetIpInfo(ipAddress);

        return response is null ? NotFound(response) : Ok(response);
    }

    /// <summary>
    /// Gets the statistics of the tracked IP addresses.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the action result.</returns>
    [HttpGet("statistic")]
    [ProducesResponseType(typeof(List<StatisticModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<StatisticModel>>> GetStatisticsAsync()
    {
        var response = await _service.GetStatistics();

        return response is null ? NotFound(response) : Ok(response);
    }

    /// <summary>
    /// Gets the average distance of the tracked IP addresses.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the action result.</returns>
    [HttpGet("average")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAverageDistanceAsync()
    {
        var response = await _service.GetAverageDistance();

        return response is null ? NotFound(response) : Ok(response);
    }
}
