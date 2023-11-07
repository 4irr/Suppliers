using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suppliers.Domain
{
    public class TenderUser
    {
        public Guid TenderId { get; set; }
        public Tender? Tender { get; set; }
        public Guid UserId { get; set; }
        public string? UserDescription { get; set; }
    }
}
