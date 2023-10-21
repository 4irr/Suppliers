using MediatR;
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

            _context.Products.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
