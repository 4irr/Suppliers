using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Suppliers.Application.Interfaces;

namespace Suppliers.Application.Tenders.Queries.GetTendersList
{
    public class GetTenderListQueryHandler : IRequestHandler<GetTenderListQuery, TendersListVm>
    {
        private readonly ISuppliersDbContext _context;
        private readonly IMapper _mapper;

        public GetTenderListQueryHandler(ISuppliersDbContext context, IMapper mapper) => 
            (_context, _mapper) = (context, mapper);

        public async Task<TendersListVm> Handle(GetTenderListQuery request, CancellationToken cancellationToken)
        {
            var tendersQuery = await _context.Tenders
                .ProjectTo<TenderLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new TendersListVm { Tenders = tendersQuery };
        }
    }
}
