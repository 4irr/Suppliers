using Microsoft.EntityFrameworkCore;
using Suppliers.Application.Interfaces;
using Suppliers.Domain;
using Suppliers.Persistence.EntityTypeConfigurations;

namespace Suppliers.Persistence
{
    public class SuppliersDbContext : DbContext, ISuppliersDbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Batch> Batches { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Tender> Tenders { get; set; }
        public DbSet<TenderUser> TendersUsers { get; set; }
        public SuppliersDbContext(DbContextOptions<SuppliersDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new TenderUserConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
