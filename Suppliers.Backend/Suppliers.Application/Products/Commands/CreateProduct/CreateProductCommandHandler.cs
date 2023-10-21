using MediatR;
using Suppliers.Application.Interfaces;
using Suppliers.Domain;

namespace Suppliers.Application.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
    {
        private readonly ISuppliersDbContext _context;

        public CreateProductCommandHandler(ISuppliersDbContext context) => _context = context;

        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                UserId = request.UserId,
                Name = request.Name,
                Price = request.Price,
                Quantity = request.Quantity,
                ExpirationDate = request.ExpirationDate
            };

            await _context.Products.AddAsync(product, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return product.Id;
        }
    }
}
