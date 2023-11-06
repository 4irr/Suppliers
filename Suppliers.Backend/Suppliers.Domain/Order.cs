using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suppliers.Domain
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid BatchId { get; set; }
        public Batch Batch { get; set; } = null!;
        public float OrderPrice { get; set; }
        public Guid SupplierId { get; set; }
    }
}
