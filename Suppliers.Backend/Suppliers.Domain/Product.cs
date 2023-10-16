using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suppliers.Domain
{
    public class Product
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public float Price { get; set; }
    }
}
