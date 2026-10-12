using Microsoft.EntityFrameworkCore;

namespace TlcBackend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Add DbSet<TEntity> properties here, for example:
        // public DbSet<User> Users { get; set; }
    }
}