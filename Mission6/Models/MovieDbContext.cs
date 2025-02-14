using Microsoft.EntityFrameworkCore;

namespace Mission6.Models
{
    public class MovieDbContext : DbContext
    {
        public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options)
        {
        }

        public DbSet<Applications> Movies { get; set; }
    }
}
