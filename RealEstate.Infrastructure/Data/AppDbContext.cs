using Microsoft.EntityFrameworkCore;
using RealEstate.Domain.Entities;

namespace RealEstate.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Property> properties { get; set; }
        public DbSet<Agent> agents { get; set; }
        public DbSet<Message> messages  { get; set; }
        public DbSet<PropertyImage> propertyImages { get; set; }
        public DbSet<PropertyReport> propertyReports { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public AppDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(AppDbContext).Assembly
            );
        }
    }
}