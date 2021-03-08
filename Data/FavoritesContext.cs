using ATP.Models;
using Microsoft.EntityFrameworkCore;

namespace ATP.Data
{
    public class FavoritesContext : DbContext
    {
        public FavoritesContext(DbContextOptions<FavoritesContext> options) : base(options)
        {
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Favorite> Favorites { get; set; }


        // override table names into using singular naming convention
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>().ToTable("Player");
            modelBuilder.Entity<Favorite>().ToTable("Favorite");
        }
    }
}
