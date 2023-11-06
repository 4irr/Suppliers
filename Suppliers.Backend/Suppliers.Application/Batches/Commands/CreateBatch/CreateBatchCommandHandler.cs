using MediatR;
using Microsoft.EntityFrameworkCore;
using Suppliers.Application.Common.Exceptions;
using Suppliers.Application.Interfaces;
using Suppliers.Domain;

namespace Suppliers.Application.Batches.Commands.CreateBatch
{
    public class CreateBatchCommandHandler : IRequestHandler<CreateBatchCommand, Guid>
    {
        private readonly ISuppliersDbContext _context;

        public CreateBatchCommandHandler(ISuppliersDbContext context) => _context = context;

        public async Task<Guid> Handle(CreateBatchCommand request, CancellationToken cancellationToken)
        {
            var product = await _context.Products.FirstOrDefaultAsync(pr => pr.Id == request.ProductId);

            if(product == null)
            {
                throw new NotFoundException(nameof(Product), request.ProductId);
            }

            product.Quantity -= request.Quantity;

            var batch = new Batch
            {
                Product = product,
                ProductId = request.ProductId,
                Quantity = request.Quantity
            };

            await _context.Batches.AddAsync(batch);
            await _context.SaveChangesAsync(cancellationToken);

            return batch.Id;
        }
    }
}
