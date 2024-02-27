using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
