using MediatR;
using Microsoft.EntityFrameworkCore;
using Suppliers.Application.Common.Exceptions;
using Suppliers.Application.Interfaces;
using Suppliers.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suppliers.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
    {
        private readonly ISuppliersDbContext _context;

        public CreateOrderCommandHandler(ISuppliersDbContext context) => _context = context;

        public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var batch = await _context.Batches.Include(batch => batch.Product).FirstOrDefaultAsync(batch => batch.Id == request.BatchId);

            if (batch == null)
            {
                throw new NotFoundException(nameof(Batch), request.BatchId);
            }

            var order = new Order
            {
                Batch = batch,
                SupplierId = request.SupplierId,
                OrderPrice = batch.Product.Price * batch.Quantity,
            };

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync(cancellationToken);

            return order.Id;
        }
    }
}
