using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suppliers.Application.Contracts.Queries.GetContractsList
{
    public class ContractListVm
    {
        public IList<ContractLookupDto>? Contracts { get; set; }
    }
}
