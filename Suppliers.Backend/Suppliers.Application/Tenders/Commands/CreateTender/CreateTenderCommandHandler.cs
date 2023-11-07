using MediatR;
using Suppliers.Application.Interfaces;
using Suppliers.Domain;

namespace Suppliers.Application.Tenders.Commands.CreateTender
{
    public class CreateTenderCommandHandler : IRequestHandler<CreateTenderCommand, Guid>
    {
        private readonly ISuppliersDbContext _context;

        public CreateTenderCommandHandler(ISuppliersDbContext context) => _context = context;

        public async Task<Guid> Handle(CreateTenderCommand request, CancellationToken cancellationToken)
        {
            var tender = new Tender
            {
                Title = request.Title,
                Description = request.Description,
                Beginning = request.Beginning,
                Ending = request.Ending,
                IsOpen = true
            };

            await _context.Tenders.AddAsync(tender);
            await _context.SaveChangesAsync(cancellationToken);

            return tender.Id;
        }
    }
}
