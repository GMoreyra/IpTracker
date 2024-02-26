using Application.ExternalServiceClients.CurrencyInfo.Models;
using Refit;
using System.Threading.Tasks;

namespace Application.ExternalServiceClients.CurrencyInfo;

/// <summary>
/// Interface for the client that communicates with the external currency information service.
/// </summary>
public interface ICurrencyInformationClient
{
    /// <summary>
    /// Retrieves the latest currency information from the external service.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the ApiResponse of CurrencyInformationResponse.</returns>
    [Get("/latest")]
    Task<ApiResponse<CurrencyInformationResponse>> GetCurrencyInformation();
}
