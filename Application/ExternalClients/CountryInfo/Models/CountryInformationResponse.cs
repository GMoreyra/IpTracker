using System.Text.Json.Serialization;

namespace Application.ExternalClients.CountryInfo.Models;

public class CountryInformationResponse
{
    public string Name { get; set; }

    public string[] TopLevelDomain { get; set; }

    public string Alpha2Code { get; set; }

    public string Alpha3Code { get; set; }

    public string[] CallingCodes { get; set; }

    public string Capital { get; set; }

    public string[] AltSpellings { get; set; }

    public string Region { get; set; }

    public string Subregion { get; set; }

    public int Population { get; set; }

    public int[] LatLng { get; set; }

    public string Demonym { get; set; }

    public int Area { get; set; }

    public float Gini { get; set; }

    public string[] Timezones { get; set; }

    public string[] Borders { get; set; }

    public string NativeName { get; set; }

    public string NumericCode { get; set; }

    public Currency[] Currencies { get; set; }

    public Language[] Languages { get; set; }

    public LanguageTranslations Translations { get; set; }

    public string Flag { get; set; }

    public RegionalBloc[] RegionalBlocs { get; set; }

    public string Cioc { get; set; }

    public class LanguageTranslations
    {
        public string Br { get; set; }

        public string De { get; set; }

        public string Es { get; set; }

        public string Fa { get; set; }

        public string Fr { get; set; }

        public string Hr { get; set; }

        public string It { get; set; }

        public string Ja { get; set; }

        public string Nl { get; set; }

        public string Pt { get; set; }
    }

    public class Currency
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string Symbol { get; set; }
    }

    public class Language
    {
        [JsonPropertyName("iso639_1")]
        public string Iso6391 { get; set; }

        [JsonPropertyName("iso639_2")]
        public string Iso6392 { get; set; }

        public string Name { get; set; }

        public string NativeName { get; set; }
    }

    public class RegionalBloc
    {
        public string Acronym { get; set; }

        public string Name { get; set; }
    }
}
