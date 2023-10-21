using MediatR;

namespace Suppliers.Application.Products.Commands.DeleteProduct
{
    public class DeleteProductCommand : IRequest
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
    }
}
