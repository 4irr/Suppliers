using Suppliers.Persistence;

namespace Suppliers.Tests.Common
{
    public class TestCommandBase : IDisposable
    {
        protected readonly SuppliersDbContext Context;

        public TestCommandBase()
        {
            Context = SuppliersDbContextFactory.Create();
        }

        public void Dispose()
        {
            SuppliersDbContextFactory.Destroy(Context);
        }
    }
}
