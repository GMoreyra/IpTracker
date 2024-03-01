namespace Application.Interfaces.IGateways;

using Application.ExternalClients.Geolocation.Models;
using System.Threading.Tasks;

public interface IGeolocationGateway
{
    Task<GeoLocationResponse> GetGeolocationInfo(string ipAddress);
}