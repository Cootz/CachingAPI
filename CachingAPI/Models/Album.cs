using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CachingAPI.Models
{
    [Table("Albums")]
    public class Album
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public User User { get; set; }
    }
}
