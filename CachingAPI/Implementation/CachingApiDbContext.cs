using CachingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CachingAPI.Implementation
{
    public class CachingApiDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Album> Albums { get; set; }

        public CachingApiDbContext(DbContextOptions<CachingApiDbContext> contextOptions)
            : base(contextOptions)
        { }
    }
}
