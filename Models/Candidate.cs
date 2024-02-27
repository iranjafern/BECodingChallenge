using System.Text.Json.Serialization;

namespace Models
{
    public class Candidate
    {
        [JsonPropertyName("name")]
        public required string Name { get; set; }

        [JsonPropertyName("phone")]
        public string? Phone { get; set; }
    }
}
