﻿using System.Text.Json.Serialization;

namespace Models
{
    public class IPLookUp
    {
        [JsonPropertyName("ip")]
        public required string IP { get; set; }

        [JsonPropertyName("hostname")]
        public string? HostName { get; set; }

        [JsonPropertyName("city")]
        public string? City { get; set; }

        [JsonPropertyName("region")]
        public string? Region { get; set; }

        [JsonPropertyName("country")]
        public string? Country { get; set; }

        [JsonPropertyName("loc")]
        public string? Loc { get; set; }

        [JsonPropertyName("org")]
        public string? Org { get; set; }

        [JsonPropertyName("postal")]
        public string? Postal { get; set; }

        [JsonPropertyName("timezone")]
        public string? Timezone { get; set; }

        [JsonPropertyName("bogon")]
        public string? Bogon { get; set; }
    }
}
