using AutoMapper;
using Suppliers.Application.Common.Mappings;
using Suppliers.Application.Suppliers.Queries.GetSuppliersList;

namespace Suppliers.Application.Reports.Commands.CreateTotalReport
{
    public class SupplierLookupDto
    {
        public string? Organization { get; set; }
        public float ContractsTotalCost { get; set; }
        public int ContractsCount { get; set; }
    }
}
