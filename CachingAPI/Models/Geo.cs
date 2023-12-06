using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CachingAPI.Models
{
    public class Geo
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }
        [JsonPropertyName("lat")]
        public double Latitude { get; set; }
        [JsonPropertyName("lng")]
        public double Longitude { get; set; }
    }
}
