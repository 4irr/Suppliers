using Suppliers.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suppliers.Application.Reports.Commands.CreateReport
{
    public class SingleReportVm
    {
        public string? Supplier { get; set; }
        public string? Beginning { get; set; }
        public string? Ending { get; set; }
        public List<ContractLookupDto>? Contracts { get; set; }
        public int ContractsCount { get; set; }
        public float TotalCost { get; set; }
    }
}
