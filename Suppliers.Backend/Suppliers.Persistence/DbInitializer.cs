using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
