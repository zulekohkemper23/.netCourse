using Microsoft.EntityFrameworkCore;

namespace _netCourse.Data
{
    public class DataContext : DbContext 
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }
        public DbSet<Character> characters { get; set; }
    }
}