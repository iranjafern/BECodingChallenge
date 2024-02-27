using System;
using System.Text.Json.Serialization;

namespace Models
{
    public class Passenger
    {
        [JsonPropertyName("name")]
        public required string Name { get; set; }

        [JsonPropertyName("pricePerPassenger")]
        public decimal? PricePerPassenger { get; set; }

        [JsonPropertyName("vehicleType")]
        public required VehicleType VehicleType { get; set; }
    }
}
