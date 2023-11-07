using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Suppliers.Application.Common.Exceptions;
using Suppliers.Application.Interfaces;
using Suppliers.Domain;

namespace Suppliers.Application.Tenders.Queries.GetTenderDetails
{
    public class GetTenderDetailsQueryHandler : IRequestHandler<GetTenderDetailsQuery, TenderDetailsVm>
    {
        private readonly ISuppliersDbContext _context;
        private readonly IMapper _mapper;

        public GetTenderDetailsQueryHandler(ISuppliersDbContext context, IMapper mapper) =>
            (_context, _mapper) = (context, mapper);

        public async Task<TenderDetailsVm> Handle(GetTenderDetailsQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Tenders.FirstOrDefaultAsync(tender => tender.Id == request.TenderId);

            if(entity == null)
            {
                throw new NotFoundException(nameof(Tender), request.TenderId);
            }

            return _mapper.Map<TenderDetailsVm>(entity);
        }
    }
}
