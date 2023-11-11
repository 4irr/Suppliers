using MediatR;
using Microsoft.EntityFrameworkCore;
using Suppliers.Application.Interfaces;

namespace Suppliers.Application.Reports.Commands.CreateTotalReport
{
    public class CreateTotalReportCommandHandler : IRequestHandler<CreateTotalReportCommand, TotalReportVm>
    {
        private readonly IUsersHttpClient _apiClient;
        private readonly ISuppliersDbContext _context;

        public CreateTotalReportCommandHandler(IUsersHttpClient apiClient, ISuppliersDbContext context)
            => (_apiClient, _context) = (apiClient, context);

        public async Task<TotalReportVm> Handle(CreateTotalReportCommand request, CancellationToken cancellationToken)
        {
            var users = await _apiClient.GetAllUsersAsync();

            var contracts = await _context.Contracts
                .Include(contract => contract.Order)
                .Where(contract => contract.IsConfirmed &&
                    (contract.ConclusionDate.Date >= request.Beginning && contract.ConclusionDate.Date <= request.Ending))
                .ToListAsync(cancellationToken);

            var suppliersList = new List<SupplierLookupDto>();

            var distinctContracts = contracts.DistinctBy(contract => contract.Order.SupplierId);

            foreach (var contract in distinctContracts)
            {
                suppliersList.Add(new SupplierLookupDto
                {
                    Organization = users.FirstOrDefault(u => u.Id == contract.Order.SupplierId.ToString())?.Organization,
                    ContractsTotalCost = contracts
                        .Where(c => c.Order.SupplierId == contract.Order.SupplierId)
                        .Sum(c => c.Order.OrderPrice),
                    ContractsCount = contracts
                        .Where(c => c.Order.SupplierId == contract.Order.SupplierId).Count()
                });
            }

            var entity = new TotalReportVm
            {
                Beginning = request.Beginning.ToLongDateString(),
                Ending = request.Ending.ToLongDateString(),
                Suppliers = suppliersList,
                SuppliersCount = suppliersList.Count(),
                TotalCost = suppliersList.Sum(supplier => supplier.ContractsTotalCost)
            };

            return entity;
        }
    }
}
