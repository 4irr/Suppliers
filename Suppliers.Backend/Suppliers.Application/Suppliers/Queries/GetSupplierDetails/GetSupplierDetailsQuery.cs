using MediatR;

namespace Suppliers.Application.Suppliers.Queries.GetSupplierDetails
{
    public class GetSupplierDetailsQuery : IRequest<SupplierDetailsVm>
    {
        public Guid Id { get; set; }
    }
}
