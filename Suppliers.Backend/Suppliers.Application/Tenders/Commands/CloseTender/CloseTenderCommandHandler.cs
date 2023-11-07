using MediatR;
using Microsoft.EntityFrameworkCore;
using Suppliers.Application.Common.Exceptions;
using Suppliers.Application.Interfaces;
using Suppliers.Domain;

namespace Suppliers.Application.Tenders.Commands.CloseTender
{
    public class CloseTenderCommandHandler : IRequestHandler<CloseTenderCommand>
    {
        private readonly ISuppliersDbContext _context;

        public CloseTenderCommandHandler(ISuppliersDbContext context) => _context = context;

        public async Task Handle(CloseTenderCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Tenders.FirstOrDefaultAsync(tender => tender.Id == request.TenderId);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Tender), request.TenderId);
            }

            entity.IsOpen = false;
            entity.ExecutorId = request.ExecutorId;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
