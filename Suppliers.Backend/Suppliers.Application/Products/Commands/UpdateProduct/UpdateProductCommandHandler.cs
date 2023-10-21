using MediatR;
using Microsoft.EntityFrameworkCore;
using Suppliers.Application.Common.Exceptions;
using Suppliers.Application.Interfaces;
using Suppliers.Domain;

namespace Suppliers.Application.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
    {
        private readonly ISuppliersDbContext _context;

        public UpdateProductCommandHandler(ISuppliersDbContext context) => _context = context;

        public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Products.FirstOrDefaultAsync(product => product.Id == request.Id, cancellationToken);

            if (entity == null || entity.UserId != request.UserId) 
            {
                throw new NotFoundException(nameof(Product), request.Id);
            }

            entity.Name = request.Name;
            entity.Price = request.Price;
            entity.Quantity = request.Quantity;
            entity.ExpirationDate = request.ExpirationDate;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
