using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CachingAPI.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public Address Address { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string WebSite { get; set; } = null!;
        public Company Company { get; set; } = null!;
    }
}
