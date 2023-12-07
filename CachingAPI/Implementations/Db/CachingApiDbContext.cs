using CachingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CachingAPI.Implementations.Db
{
    public class CachingApiDbContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Album> Albums { get; set; } = null!;

        public CachingApiDbContext(DbContextOptions<CachingApiDbContext> contextOptions)
            : base(contextOptions)
        { }
    }
}
