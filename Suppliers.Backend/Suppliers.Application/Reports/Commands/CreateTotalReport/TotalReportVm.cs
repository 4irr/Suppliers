using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suppliers.Application.Reports.Commands.CreateTotalReport
{
    public class TotalReportVm
    {
        public string? Beginning { get; set; }
        public string? Ending { get; set; }
        public List<SupplierLookupDto>? Suppliers { get; set; }
        public int SuppliersCount { get; set; }
        public float TotalCost { get; set; }
    }
}
