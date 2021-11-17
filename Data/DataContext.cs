using _netCourse.Models;
using Microsoft.EntityFrameworkCore;

namespace _netCourse.Data
{
    public class DataContext : DbContext 
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }
        public DbSet<Character> characters { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Weapons> weapons { get; set; }
        public DbSet<Skills> skills { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Skills>().HasData(
                new Skills {Id = 1, name = "Fireball", damage = 30},
                new Skills {Id = 2, name = "Frenzy", damage = 20},
                new Skills {Id = 3, name = "Blizzard", damage = 50}
            ); 
        }
    }
}