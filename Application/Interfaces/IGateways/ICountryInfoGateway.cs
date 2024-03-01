﻿namespace Application.Interfaces.IGateways;

using Application.ExternalClients.CountryInfo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICountryInfoGateway
{
    Task<IEnumerable<CountryInformationResponse>> GetCountryInfo();
}