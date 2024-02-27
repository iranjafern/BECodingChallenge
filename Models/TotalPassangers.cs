using System.Text.Json.Serialization;

namespace Models
{
    public class TotalPassangers
    {
        [JsonPropertyName("total")]
        public decimal? Total { get; set; }

        [JsonPropertyName("passengers")]
        public List<Passenger>? Passengers { get; set; }

    }
}
