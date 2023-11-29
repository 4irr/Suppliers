using AutoMapper;
using Suppliers.Application.Common.Mappings;
using Suppliers.Application.Interfaces;
using Suppliers.Persistence;
using Xunit;

namespace Suppliers.Tests.Common
{
    public class QueryTestFixture : IDisposable
    {
        public SuppliersDbContext Context;
        public IMapper Mapper;

        public QueryTestFixture()
        {
            Context = SuppliersDbContextFactory.Create();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AssemblyMappingProfile(
                    typeof(ISuppliersDbContext).Assembly));
            });
            Mapper = configurationProvider.CreateMapper();
        }

        public void Dispose()
        {
            SuppliersDbContextFactory.Destroy(Context);
        }
    }

    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
}
