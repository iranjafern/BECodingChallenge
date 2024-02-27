using System.Text.Json.Serialization;

namespace Models
{
    public class Quote
    {
        [JsonPropertyName("from")]
        public required string From { get; set; }

        [JsonPropertyName("to")]
        public string? To { get; set; }

        [JsonPropertyName("listings")]
        public required List<Passenger> Passengers { get; set; }

    }
}
