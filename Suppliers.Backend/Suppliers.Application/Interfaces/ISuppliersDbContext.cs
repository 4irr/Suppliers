using Microsoft.EntityFrameworkCore;
using Suppliers.Domain;

namespace Suppliers.Application.Interfaces
{
    public interface ISuppliersDbContext
    {
        DbSet<Product> Products { get; set; }
        DbSet<Batch> Batches { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<Contract> Contracts { get; set; }
        DbSet<Tender> Tenders { get; set; }
        DbSet<TenderUser> TendersUsers { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
