using MediatR;
using Suppliers.Application.Common.Exceptions;
using Suppliers.Application.Interfaces;
using Suppliers.Domain;

namespace Suppliers.Application.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
    {
        private readonly ISuppliersDbContext _context;

        public DeleteOrderCommandHandler(ISuppliersDbContext context) => _context = context;

        public async Task Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Orders.FindAsync(new object[] { request.OrderId }, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Order), request.OrderId);
            }

            _context.Orders.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
