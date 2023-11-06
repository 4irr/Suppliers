using MediatR;
using Microsoft.EntityFrameworkCore;
using Suppliers.Application.Common.Exceptions;
using Suppliers.Application.Interfaces;
using Suppliers.Domain;

namespace Suppliers.Application.Contracts.Commands.CreateContract
{
    public class CreateContractCommandHandler : IRequestHandler<CreateContractCommand, Guid>
    {
        private readonly ISuppliersDbContext _context;

        public CreateContractCommandHandler(ISuppliersDbContext context) => _context = context;

        public async Task<Guid> Handle(CreateContractCommand request, CancellationToken cancellationToken)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(order => order.Id == request.OrderId);

            if(order == null)
            {
                throw new NotFoundException(nameof(Order), request.OrderId);
            }

            var contract = new Contract
            {
                Order = order,
                ConclusionDate = DateTime.Now,
                IsConfirmed = false
            };

            await _context.Contracts.AddAsync(contract);
            await _context.SaveChangesAsync(cancellationToken);

            return contract.Id;
        }
    }
}
