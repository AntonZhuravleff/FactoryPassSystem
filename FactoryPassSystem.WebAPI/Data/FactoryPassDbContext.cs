using FactoryPassSystem.WebAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace FactoryPassSystem.WebAPI.Data
{
    public class FactoryPassDbContext : DbContext
    {
        public DbSet<Position> Positions { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Shift> Shifts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=factoryPassSystem.db");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Employee>()
                .HasIndex(e => e.PassId)
                .IsUnique();
        }
    }
}
