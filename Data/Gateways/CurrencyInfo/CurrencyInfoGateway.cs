namespace Data.Gateways.CurrencyInfo;

using Application.ExternalServiceClients.CurrencyInfo;
using System;

public class CurrencyInfoGateway
{
    private readonly ICurrencyInformationClient _currencyInformationClient;

    public CurrencyInfoGateway(ICurrencyInformationClient currencyInformationClient)
    {
        _currencyInformationClient = currencyInformationClient ?? throw new ArgumentNullException(nameof(currencyInformationClient));
    }
}
