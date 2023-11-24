using Suppliers.Application.Products.Queries.GetProductList;
using Suppliers.Application.Reports.Commands.CreateReport;

namespace Suppliers.Application.Users.Commands.CalculateUserActivity
{
    public class UserActivityVm
    {
        public string? Supplier { get; set; }
        public string? Beginning { get; set; }
        public string? Ending { get; set; }
        public List<ProductLookupDto>? Products { get; set; }
        public List<ContractLookupDto>? Contracts { get; set; }
        public List<TenderUserLookupDto>? Tenders { get; set; }
    }
}
