using MediatR;

namespace Suppliers.Application.Reports.Commands.CreateReport
{
    public class CreateSingleReportCommand : IRequest<SingleReportVm>
    {
        public Guid SupplierId { get; set; }
        public DateTime Beginning { get; set; }
        public DateTime Ending { get; set; }
    }
}
