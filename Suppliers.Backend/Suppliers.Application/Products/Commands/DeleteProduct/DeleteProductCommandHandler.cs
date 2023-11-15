using MediatR;
using Microsoft.EntityFrameworkCore;
using Suppliers.Application.Common.Exceptions;
using Suppliers.Application.Interfaces;
using Suppliers.Domain;

namespace Suppliers.Application.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly ISuppliersDbContext _context;

        public DeleteProductCommandHandler(ISuppliersDbContext context) => _context = context;

        public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Products.FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Product), request.Id);
            }

            var batches = await _context.Batches.Include(batch => batch.Product).ToListAsync(cancellationToken);

            if(batches.FirstOrDefault(batch => batch.ProductId == entity.Id) != null)
            {
                throw new Exception("Невозможно удалить информацию о товаре, для которого существует активный заказ");
            }

            _context.Products.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
