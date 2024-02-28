namespace Data.Gateways.CurrencyInfo.Interface;

using Application.ExternalServiceClients.CurrencyInfo.Models;
using System.Threading.Tasks;

public interface ICurrencyInfoGateway
{
    Task<CurrencyInformationResponse> GetCurrencyInfo();
}