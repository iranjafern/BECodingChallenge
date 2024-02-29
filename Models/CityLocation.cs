using System.Text.Json.Serialization;

namespace Models
{
    public class CityLocation
    {        
        [JsonPropertyName("city")]
        public string? City { get; set; }

        [JsonPropertyName("loc")]
        public string? Loc { get; set; }

        [JsonPropertyName("isValidIP")]
        public bool IsValidIP { get; set; }
    }
}
