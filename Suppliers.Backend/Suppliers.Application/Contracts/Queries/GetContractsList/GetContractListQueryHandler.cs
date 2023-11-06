using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Suppliers.Application.Interfaces;

namespace Suppliers.Application.Contracts.Queries.GetContractsList
{
    public class GetContractListQueryHandler : IRequestHandler<GetContractsListQuery, ContractListVm>
    {
        private readonly ISuppliersDbContext _context;
        private readonly IMapper _mapper;

        public GetContractListQueryHandler(ISuppliersDbContext context, IMapper mapper) => 
            (_context, _mapper) = (context, mapper);

        public async Task<ContractListVm> Handle(GetContractsListQuery request, CancellationToken cancellationToken)
        {
            var contractsQuery = await _context.Contracts
                .Include(contract => contract.Order)
                .ThenInclude(order => order.Batch)
                .ThenInclude(batch => batch.Product)
                .ProjectTo<ContractLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new ContractListVm { Contracts = contractsQuery };
        }
    }
}
