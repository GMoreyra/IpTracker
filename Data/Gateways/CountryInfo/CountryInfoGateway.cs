namespace Data.Gateways.CountryInfo;

using Application.ExternalServiceClients.CountryInfo;
using Application.ExternalServiceClients.CountryInfo.Models;
using Application.IGateways;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class CountryInfoGateway : ICountryInfoGateway
{
    private readonly ICountryInformationClient _countryInformationClient;

    public CountryInfoGateway(ICountryInformationClient countryInformationClient)
    {
        _countryInformationClient = countryInformationClient ?? throw new ArgumentNullException(nameof(countryInformationClient));
    }

    public async Task<IEnumerable<CountryInformationResponse>> GetCountryInfo()
    {
        var countryInformation = await _countryInformationClient.GetAllCountriesInformation();

        return countryInformation.Content;
    }
}
