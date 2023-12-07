using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CachingAPI.Models
{
    [Table("Albums")]
    public class Album
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; init; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }

        [JsonIgnore]
        public User User { get; init; }
    }
}
