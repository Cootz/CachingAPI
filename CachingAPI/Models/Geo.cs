using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CachingAPI.Models
{
    public class Geo
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
