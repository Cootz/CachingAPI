using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CachingAPI.Models
{
    public class Company
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; }
        public string CatchPhrase { get; set; }
        public string BS {  get; set; }
    }
}
