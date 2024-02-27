namespace Application.Options;

public class ExternalServiceOptions
{
    public const string ExternalServiceSectionName = "ExternalServiceClients";

    public required string CountryClient { get; set; }

    public required string CurrencyClient { get; set; }

    public required string GeoLocationClient { get; set; }
}
