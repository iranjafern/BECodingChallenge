using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
