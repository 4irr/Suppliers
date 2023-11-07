using MediatR;

namespace Suppliers.Application.Tenders.Commands.CreateTender
{
    public class CreateTenderCommand : IRequest<Guid>
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime Beginning { get; set; }
        public DateTime Ending { get; set; }
    }
}
