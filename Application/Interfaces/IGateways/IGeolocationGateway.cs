namespace Application.Interfaces.IGateways;

using Application.ExternalServiceClients.Geolocation.Models;
using System.Threading.Tasks;

public interface IGeolocationGateway
{
    Task<GeoLocationResponse> GetGeolocationInfo(string ipAddress);
}