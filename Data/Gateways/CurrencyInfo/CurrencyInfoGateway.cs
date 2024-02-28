namespace Data.Gateways.CurrencyInfo;

using Application.ExternalServiceClients.CurrencyInfo;
using Application.ExternalServiceClients.CurrencyInfo.Models;
using Data.Gateways.CurrencyInfo.Interface;
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
