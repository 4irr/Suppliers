using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suppliers.Application.Tenders.Queries.GetTendersList
{
    public class TendersListVm
    {
        public IList<TenderLookupDto>? Tenders { get; set; }
    }
}
