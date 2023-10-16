using Microsoft.EntityFrameworkCore;
using Suppliers.Domain;

namespace Suppliers.Application.Interfaces
{
    public interface ISuppliersDbContext
    {
        DbSet<Product> Products { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
