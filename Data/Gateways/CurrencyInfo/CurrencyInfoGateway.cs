namespace Infrastructure.Gateways.CurrencyInfo;

using Application.ExternalClients.CurrencyInfo;
using Application.ExternalClients.CurrencyInfo.Models;
using Application.Interfaces.IGateways;
using System;
using System.Threading.Tasks;

public class CurrencyInfoGateway : ICurrencyInfoGateway
{
    private readonly ICurrencyInformationClient _currencyInformationClient;

    public CurrencyInfoGateway(ICurrencyInformationClient currencyInformationClient)
    {
        _currencyInformationClient = currencyInformationClient ?? throw new ArgumentNullException(nameof(currencyInformationClient));
    }

    public async Task<CurrencyInformationResponse> GetCurrencyInfo()
    {
        var currencyInformation = await _currencyInformationClient.GetCurrencyInformation();

        return currencyInformation.Content;
    }
}
