using System.Text.Json.Serialization;

namespace Models
{
    public class VehicleType
    {
        [JsonPropertyName("name")]
        public required string Name { get; set; }

        [JsonPropertyName("maxPassengers")]
        public required int MaxPassengers { get; set; }
    }
}
