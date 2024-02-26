using System.Text.Json.Serialization;

namespace Api.Contracts
{
    public class GetGeolocationResponse
    {
        public required string IP { get; set; }

        [JsonPropertyName("current_date")]
        public required DateTime CurrentDate { get; set; }

        public required string Country { get; set; }

        [JsonPropertyName("iso_code")]
        public required string ISOCode { get; set; }

        public required string[] Languages { get; set; }

        public required string Currency { get; set; }

        public required string Time { get; set; }

        [JsonPropertyName("estimated_distance")]
        public required string EstimatedDistance { get; set; }
    }
}
