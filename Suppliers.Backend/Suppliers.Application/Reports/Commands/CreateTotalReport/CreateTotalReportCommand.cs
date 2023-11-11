using MediatR;

namespace Suppliers.Application.Reports.Commands.CreateTotalReport
{
    public class CreateTotalReportCommand : IRequest<TotalReportVm>
    {
        public DateTime Beginning { get; set; }
        public DateTime Ending { get; set; }
    }
}
