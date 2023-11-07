using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Suppliers.Domain;

namespace Suppliers.Persistence.EntityTypeConfigurations
{
    public class TenderUserConfiguration : IEntityTypeConfiguration<TenderUser>
    {
        public void Configure(EntityTypeBuilder<TenderUser> builder)
        {
            builder.HasKey(t => new { t.TenderId, t.UserId });
        }
    }
}
