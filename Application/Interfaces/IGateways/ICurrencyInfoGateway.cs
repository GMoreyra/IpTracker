namespace Application.Interfaces.IGateways;

using Application.ExternalClients.CurrencyInfo.Models;
using System.Threading.Tasks;

public interface ICurrencyInfoGateway
{
    Task<CurrencyInformationResponse> GetCurrencyInfo();
}