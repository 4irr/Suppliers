using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Suppliers.Domain;

namespace Suppliers.Persistence.EntityTypeConfigurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(product => product.Id);
            builder.HasIndex(product => product.Id).IsUnique();
            builder.Property(product => product.Name).HasMaxLength(250);
        }
    }
}
