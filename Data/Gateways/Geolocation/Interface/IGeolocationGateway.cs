namespace Data.Gateways.Geolocation.Interface;

using Application.ExternalServiceClients.Geolocation.Models;
using System.Threading.Tasks;

public interface IGeolocationGateway
{
    Task<GeoLocationResponse> GetGeolocationInfo(string ipAddress);
}