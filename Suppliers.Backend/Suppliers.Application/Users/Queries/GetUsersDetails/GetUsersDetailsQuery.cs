using MediatR;

namespace Suppliers.Application.Suppliers.Queries.GetSupplierDetails
{
    public class GetUsersDetailsQuery : IRequest<UserDetailsVm>
    {
        public Guid Id { get; set; }
    }
}
