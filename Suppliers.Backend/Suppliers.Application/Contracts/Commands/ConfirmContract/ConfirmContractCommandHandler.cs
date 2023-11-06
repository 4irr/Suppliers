using MediatR;
using Microsoft.EntityFrameworkCore;
using Suppliers.Application.Common.Exceptions;
using Suppliers.Application.Interfaces;
using System.Diagnostics.Contracts;

namespace Suppliers.Application.Contracts.Commands.ConfirmContract
{
    public class ConfirmContractCommandHandler : IRequestHandler<ConfirmContractCommand>
    {
        private readonly ISuppliersDbContext _context;

        public ConfirmContractCommandHandler(ISuppliersDbContext context) => _context = context;

        public async Task Handle(ConfirmContractCommand request, CancellationToken cancellationToken)
        {
            var contract = await _context.Contracts.FirstOrDefaultAsync(contract => contract.Id == request.ContractId);

            if (contract == null || contract.IsConfirmed) 
            {
                throw new NotFoundException(nameof(Contract), request.ContractId);
            }

            contract.IsConfirmed = true;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
