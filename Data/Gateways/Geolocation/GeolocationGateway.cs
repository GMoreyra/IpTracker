namespace Infrastructure.Gateways.Geolocation;

using Application.ExternalServiceClients.Geolocation;
using Application.ExternalServiceClients.Geolocation.Models;
using Application.IGateways;
using System;
using System.Threading.Tasks;

public class GeolocationGateway : IGeolocationGateway
{
    private readonly IGeoLocationClient _geolocationClient;

    public GeolocationGateway(IGeoLocationClient geolocationClient)
    {
        _geolocationClient = geolocationClient ?? throw new ArgumentNullException(nameof(geolocationClient));
    }

    public async Task<GeoLocationResponse> GetGeolocationInfo(string ipAddress)
    {
        var geolocationInformation = await _geolocationClient.GetGeoLocationInformation(ipAddress);

        return geolocationInformation.Content;
    }
}
