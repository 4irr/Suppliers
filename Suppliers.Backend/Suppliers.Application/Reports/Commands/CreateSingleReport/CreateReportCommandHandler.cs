using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Suppliers.Application.Common.Exceptions;
using Suppliers.Application.Interfaces;
using Suppliers.Application.Reports.Commands.CreateReport;
using Suppliers.Application.Suppliers.Queries.GetSuppliersList;

namespace Suppliers.Application.Reports.Commands.CreateSingleReport
{
    public class CreateReportCommandHandler : IRequestHandler<CreateSingleReportCommand, SingleReportVm>
    {
        private readonly IUsersHttpClient _apiClient;
        private readonly ISuppliersDbContext _context;
        private readonly IMapper _mapper;

        public CreateReportCommandHandler(IUsersHttpClient apiClient, ISuppliersDbContext context, IMapper mapper) =>
            (_apiClient, _context, _mapper) = (apiClient, context, mapper);

        public async Task<SingleReportVm> Handle(CreateSingleReportCommand request, CancellationToken cancellationToken)
        {
            var user = await _apiClient.GetUserByIdAsync(request.SupplierId);

            if (user == null)
            {
                throw new NotFoundException(nameof(AppUserDto), request.SupplierId);
            }

            var contracts = await _context.Contracts
                .Where(contract => contract.Order.SupplierId == request.SupplierId && contract.IsConfirmed &&
                    (contract.ConclusionDate.Date >= request.Beginning && contract.ConclusionDate.Date <= request.Ending))
                .ProjectTo<ContractLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var entity = new SingleReportVm
            {
                Supplier = user.Organization,
                Beginning = request.Beginning.ToLongDateString(),
                Ending = request.Ending.ToLongDateString(),
                Contracts = contracts,
                ContractsCount = contracts.Count,
                TotalCost = contracts.Sum(dto => dto.OrderPrice)
            };

            return entity;

        }
    }
}
