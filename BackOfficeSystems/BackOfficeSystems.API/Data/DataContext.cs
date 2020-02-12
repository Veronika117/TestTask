using BackOfficeSystems.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BackOfficeSystems.API.Data
{
    public class DataContext : DbContext
    {
         public DbSet<Brand> Brands { get; set; }
         public DbSet<Order> Orders { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Brand>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.HasIndex(x => x.Name).IsUnique();
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.BrandId).IsRequired();
                entity.Property(e => e.Quantity).IsRequired();
                entity.Property(e => e.TimeOrdered).IsRequired();
            });
        }
    }
}