using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CachingAPI.Models
{
    public class Company
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string CatchPhrase { get; set; } = null!;
        public string BS {  get; set; } = null!;
    }
}
