using MediatR;

namespace Suppliers.Application.Products.Queries.GetProductDetails
{
    public class GetProductDetailsQuery : IRequest<ProductDetailsVm>
    {
        public Guid Id { get; set; }
    }
}
