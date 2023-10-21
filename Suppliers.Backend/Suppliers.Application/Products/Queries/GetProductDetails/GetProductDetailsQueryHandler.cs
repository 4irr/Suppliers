using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Suppliers.Application.Common.Exceptions;
using Suppliers.Application.Interfaces;
using Suppliers.Domain;

namespace Suppliers.Application.Products.Queries.GetProductDetails
{
    public class GetProductDetailsQueryHandler : IRequestHandler<GetProductDetailsQuery, ProductDetailsVm>
    {
        private readonly ISuppliersDbContext _context;
        private readonly IMapper _mapper;

        public GetProductDetailsQueryHandler(ISuppliersDbContext context, IMapper mapper) => 
            (_context, _mapper) = (context, mapper);

        public async Task<ProductDetailsVm> Handle(GetProductDetailsQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Products.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
            
            if (entity == null) 
            {
                throw new NotFoundException(nameof(Product), request.Id);
            }

            return _mapper.Map<ProductDetailsVm>(entity);
        }
    }
}
