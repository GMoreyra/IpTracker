using Application.ExternalServiceClients.Geolocation.Models;
using Refit;
using System.Threading.Tasks;

namespace Application.ExternalServiceClients.Geolocation;

/// <summary>
/// Interface for the GeoLocation client.
/// </summary>
public interface IGeoLocationClient
{
    /// <summary>
    /// Gets the geolocation information for the given IP address.
    /// </summary>
    /// <param name="ipAddress">The IP address to get geolocation information for.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the ApiResponse of GeoLocationResponse.</returns>
    [Get("/{ipAddress}")]
    Task<ApiResponse<GeoLocationResponse>> GetGeoLocationInformation(string ipAddress);
}
