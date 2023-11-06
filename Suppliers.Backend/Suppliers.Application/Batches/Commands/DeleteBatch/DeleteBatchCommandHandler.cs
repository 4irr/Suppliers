using MediatR;
using Suppliers.Application.Common.Exceptions;
using Suppliers.Application.Interfaces;
using Suppliers.Domain;

namespace Suppliers.Application.Batches.Commands.DeleteBatch
{
    public class DeleteBatchCommandHandler : IRequestHandler<DeleteBatchCommand>
    {
        private readonly ISuppliersDbContext _context;

        public DeleteBatchCommandHandler(ISuppliersDbContext context) => _context = context;

        public async Task Handle(DeleteBatchCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Batches.FindAsync(new object[] { request.BatchId}, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Batch), request.BatchId);
            }

            _context.Batches.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
