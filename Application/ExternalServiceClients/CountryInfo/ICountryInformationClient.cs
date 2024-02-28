using Application.ExternalServiceClients.CountryInfo.Models;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.ExternalServiceClients.CountryInfo;

/// <summary>
/// Interface for the client that communicates with the Country Information external service.
/// </summary>
public interface ICountryInformationClient
{
    /// <summary>
    /// Retrieves information about all countries from the external service.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the ApiResponse of CountryInformationResponse.</returns>
    [Get("/all")]
    Task<ApiResponse<IEnumerable<CountryInformationResponse>>> GetAllCountriesInformation();
}
