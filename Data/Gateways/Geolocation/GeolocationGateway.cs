namespace Data.Gateways.Geolocation;

using Application.ExternalServiceClients.Geolocation;
using System;

public class GeolocationGateway
{
    private readonly IGeoLocationClient _geolocationClient;

    public GeolocationGateway(IGeoLocationClient geolocationClient)
    {
        _geolocationClient = geolocationClient ?? throw new ArgumentNullException(nameof(geolocationClient));
    }
}
