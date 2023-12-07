using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CachingAPI.Models
{
    public class Address
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }
        public string Street { get; set; } = null!;
        public string Suite { get; set; } = null!;
        public string City { get; set; } = null!;
        public string ZipCode { get; set; } = null!;
        public Geo Geo { get; set; } = null!;
    }
}
