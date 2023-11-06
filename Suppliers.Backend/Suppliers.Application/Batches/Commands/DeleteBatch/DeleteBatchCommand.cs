using MediatR;

namespace Suppliers.Application.Batches.Commands.DeleteBatch
{
    public class DeleteBatchCommand : IRequest
    {
        public Guid BatchId { get; set; }
    }
}
