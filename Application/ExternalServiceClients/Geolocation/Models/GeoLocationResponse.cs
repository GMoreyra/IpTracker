using System;
using System.Text.Json.Serialization;

namespace Application.ExternalServiceClients.Geolocation.Models;

public class GeoLocationResponse
{
    public string Ip { get; set; }

    public string Hostname { get; set; }

    public string Type { get; set; }

    [JsonPropertyName("continent_code")]
    public string ContinentCode { get; set; }

    [JsonPropertyName("continent_name")]
    public string ContinentName { get; set; }

    [JsonPropertyName("country_code")]
    public string CountryCode { get; set; }

    [JsonPropertyName("country_name")]
    public string CountryName { get; set; }

    [JsonPropertyName("region_code")]
    public string RegionCode { get; set; }

    [JsonPropertyName("region_name")]
    public string RegionName { get; set; }

    public string City { get; set; }

    public string Zip { get; set; }

    public float Latitude { get; set; }

    public float Longitude { get; set; }

    public LocationInformation Location { get; set; }

    [JsonPropertyName("time_zone")]
    public TimeZoneInformation TimeZone { get; set; }

    public CurrencyInformation Currency { get; set; }

    public ConnectionInformation Connection { get; set; }

    public SecurityInformation Security { get; set; }

    public class LocationInformation
    {
        [JsonPropertyName("geoname_id")]
        public int GeonameId { get; set; }

        public string Capital { get; set; }

        public Language[] Languages { get; set; }

        [JsonPropertyName("country_flag")]
        public string CountryFlag { get; set; }

        [JsonPropertyName("country_flag_emoji")]
        public string CountryFlagEmoji { get; set; }

        [JsonPropertyName("country_flag_emoji_unicode")]
        public string CountryFlagEmojiUnicode { get; set; }

        [JsonPropertyName("calling_code")]
        public string CallingCode { get; set; }

        [JsonPropertyName("is_eu")]
        public bool IsEu { get; set; }
    }

    public class Language
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string Native { get; set; }
    }

    public class TimeZoneInformation
    {
        public string Id { get; set; }

        [JsonPropertyName("current_time")]
        public DateTime CurrentTime { get; set; }

        [JsonPropertyName("gmt_offset")]
        public int GmtOffset { get; set; }

        public string Code { get; set; }

        [JsonPropertyName("is_daylight_saving")]
        public bool IsDaylightSaving { get; set; }
    }

    public class CurrencyInformation
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string Plural { get; set; }

        public string Symbol { get; set; }

        [JsonPropertyName("symbol_native")]
        public string SymbolNative { get; set; }
    }

    public class ConnectionInformation
    {
        public int Asn { get; set; }

        public string Isp { get; set; }
    }

    public class SecurityInformation
    {
        [JsonPropertyName("is_proxy")]
        public bool IsProxy { get; set; }

        [JsonPropertyName("proxy_type")]
        public object ProxyType { get; set; }

        [JsonPropertyName("is_crawler")]
        public bool IsCrawler { get; set; }

        [JsonPropertyName("crawler_name")]
        public object CrawlerName { get; set; }

        [JsonPropertyName("crawler_type")]
        public object CrawlerType { get; set; }

        [JsonPropertyName("is_tor")]
        public bool IsTor { get; set; }

        [JsonPropertyName("threat_level")]
        public string ThreatLevel { get; set; }

        [JsonPropertyName("threat_types")]
        public object ThreatTypes { get; set; }
    }
}
