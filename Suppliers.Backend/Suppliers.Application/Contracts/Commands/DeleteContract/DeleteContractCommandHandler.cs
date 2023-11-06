using MediatR;
using Microsoft.EntityFrameworkCore;
using Suppliers.Application.Common.Exceptions;
using Suppliers.Application.Interfaces;
using Suppliers.Domain;

namespace Suppliers.Application.Contracts.Commands.DeleteContract
{
    public class DeleteContractCommandHandler : IRequestHandler<DeleteContractCommand>
    {
        private readonly ISuppliersDbContext _context;

        public DeleteContractCommandHandler(ISuppliersDbContext context) => _context = context;

        public async Task Handle(DeleteContractCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Contracts
                .Include(contract => contract.Order)
                .ThenInclude(order => order.Batch)
                .FirstOrDefaultAsync(contract => contract.Id == request.ContractId);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Contract), request.ContractId);
            }

            if (!entity.IsConfirmed)
            {
                var product = await _context.Products.FirstOrDefaultAsync(product => product.Id == entity.Order.Batch.ProductId);
                if (product != null) 
                {
                    product.Quantity += entity.Order.Batch.Quantity;
                }
                await _context.SaveChangesAsync(cancellationToken);
            }

            _context.Contracts.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
