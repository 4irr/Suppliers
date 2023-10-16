using Microsoft.EntityFrameworkCore;
using Suppliers.Application.Interfaces;
using Suppliers.Domain;
using Suppliers.Persistence.EntityTypeConfigurations;

namespace Suppliers.Persistence
{
    public class SuppliersDbContext : DbContext, ISuppliersDbContext
    {
        public DbSet<Product> Products { get; set; }

        public SuppliersDbContext(DbContextOptions<SuppliersDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
