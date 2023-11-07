using MediatR;
using Microsoft.EntityFrameworkCore;
using Suppliers.Application.Common.Exceptions;
using Suppliers.Application.Interfaces;
using Suppliers.Domain;

namespace Suppliers.Application.Tenders.Commands.DeleteTender
{
    public class DeleteTenderCommandHandler : IRequestHandler<DeleteTenderCommand>
    {
        private readonly ISuppliersDbContext _context;

        public DeleteTenderCommandHandler(ISuppliersDbContext context) => _context = context;

        public async Task Handle(DeleteTenderCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Tenders.FirstOrDefaultAsync(tender => tender.Id == request.TenderId);

            if(entity == null)
            {
                throw new NotFoundException(nameof(Tender), request.TenderId);
            }

            _context.Tenders.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
