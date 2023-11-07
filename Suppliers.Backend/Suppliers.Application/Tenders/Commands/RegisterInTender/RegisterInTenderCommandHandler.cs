using MediatR;
using Microsoft.EntityFrameworkCore;
using Suppliers.Application.Common.Exceptions;
using Suppliers.Application.Interfaces;
using Suppliers.Domain;

namespace Suppliers.Application.Tenders.Commands.RegisterInTender
{
    public class RegisterInTenderCommandHandler : IRequestHandler<RegisterInTenderCommand>
    {
        private readonly ISuppliersDbContext _context;

        public RegisterInTenderCommandHandler(ISuppliersDbContext context) => _context = context;

        public async Task Handle(RegisterInTenderCommand request, CancellationToken cancellationToken)
        {
            var tender = await _context.Tenders.FirstOrDefaultAsync(tender => tender.Id == request.TenderId);

            if (tender == null)
            {
                throw new NotFoundException(nameof(Tender), request.TenderId);
            }

            var tenderUser = new TenderUser
            {
                TenderId = request.TenderId,
                UserId = request.UserId,
                Tender = tender,
                UserDescription= request.UserDescription
            };

            await _context.TendersUsers.AddAsync(tenderUser);

            tender.tenderUsers?.Add(tenderUser);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
