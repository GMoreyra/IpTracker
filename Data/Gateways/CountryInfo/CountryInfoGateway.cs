namespace Data.Gateways.CountryInfo;

using Application.ExternalServiceClients.CountryInfo;
using System;

public class CountryInfoGateway
{
    private readonly ICountryInformationClient _countryInformationClient;

    public CountryInfoGateway(ICountryInformationClient countryInformationClient)
    {
        _countryInformationClient = countryInformationClient ?? throw new ArgumentNullException(nameof(countryInformationClient));
    }
}
