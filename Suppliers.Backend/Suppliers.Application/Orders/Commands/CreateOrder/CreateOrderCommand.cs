using MediatR;

namespace Suppliers.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<Guid>
    {
        public Guid BatchId { get; set; }
        public Guid SupplierId { get; set; }
    }
}
