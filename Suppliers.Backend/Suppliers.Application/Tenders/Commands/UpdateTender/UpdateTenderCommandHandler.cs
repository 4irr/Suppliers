using MediatR;
using Microsoft.EntityFrameworkCore;
using Suppliers.Application.Common.Exceptions;
using Suppliers.Application.Interfaces;
using Suppliers.Domain;

namespace Suppliers.Application.Tenders.Commands.UpdateTender
{
    public class UpdateTenderCommandHandler : IRequestHandler<UpdateTenderCommand>
    {
        private readonly ISuppliersDbContext _context;

        public UpdateTenderCommandHandler(ISuppliersDbContext context) => _context = context;

        public async Task Handle(UpdateTenderCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Tenders.FirstOrDefaultAsync(tender => tender.Id == request.Id);

            if(entity == null) 
            {
                throw new NotFoundException(nameof(Tender), request.Id);
            }

            entity.Title = request.Title;
            entity.Description = request.Description;
            entity.Beginning = request.Beginning;
            entity.Ending = request.Ending;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
