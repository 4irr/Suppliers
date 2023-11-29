namespace Suppliers.Persistence
{
    public class DbInitializer
    {
        public static void Initialize(SuppliersDbContext context) 
        {
            context.Database.EnsureCreated();
        }
    }
}
