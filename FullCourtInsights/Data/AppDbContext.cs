using FullCourtInsights.Models;
using Microsoft.EntityFrameworkCore;

namespace FullCourtInsights.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

    }
}
