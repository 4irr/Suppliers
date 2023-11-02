using Suppliers.Application.Products.Queries.GetProductList;

namespace Suppliers.Application.Suppliers.Queries.GetSuppliersList
{
    public class SuppliersListVm
    {
        public IList<AppUserDto>? Suppliers { get; set; }
    }
}
