using MediatR;

namespace Suppliers.Application.Batches.Commands.CreateBatch
{
    public class CreateBatchCommand : IRequest<Guid>
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
