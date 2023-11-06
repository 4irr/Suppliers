using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Suppliers.Application.Interfaces;

namespace Suppliers.Application.Contracts.Queries.GetContractsList.GetUserContractsList
{
    public class GetUserContractListQueryHandler : IRequestHandler<GetUserContractsListQuery, ContractListVm>
    {
        private readonly ISuppliersDbContext _context;
        private readonly IMapper _mapper;

        public GetUserContractListQueryHandler(ISuppliersDbContext context, IMapper mapper) =>
            (_context, _mapper) = (context, mapper);

        public async Task<ContractListVm> Handle(GetUserContractsListQuery request, CancellationToken cancellationToken)
        {
            var contractsQuery = await _context.Contracts
                .Include(contract => contract.Order)
                .ThenInclude(order => order.Batch)
                .ThenInclude(batch => batch.Product)
                .Where(contract => contract.Order.SupplierId == request.UserId)
                .ProjectTo<ContractLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new ContractListVm { Contracts = contractsQuery };
        }
    }
}
