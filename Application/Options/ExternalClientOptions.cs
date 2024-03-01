namespace Application.Options;

public class ExternalClientOptions
{
    public const string ExternalServiceSectionName = "ExternalClients";

    public required string CountryClient { get; set; }

    public required string CurrencyClient { get; set; }

    public required string GeoLocationClient { get; set; }
}
