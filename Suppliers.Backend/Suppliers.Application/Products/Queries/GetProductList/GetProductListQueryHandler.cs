using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Suppliers.Application.Interfaces;

namespace Suppliers.Application.Products.Queries.GetProductList
{
    public class GetProductListQueryHandler : IRequestHandler<GetProductListQuery, ProductListVm>
    {
        private readonly ISuppliersDbContext _context;
        private readonly IMapper _mapper;

        public GetProductListQueryHandler(ISuppliersDbContext context, IMapper mapper) =>
            (_context, _mapper) = (context, mapper);

        public async Task<ProductListVm> Handle(GetProductListQuery request, CancellationToken cancellationToken)
        {
            var productsQuery = await _context.Products
                .Where(product => product.UserId == request.UserId)
                .ProjectTo<ProductLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new ProductListVm { Products = productsQuery };
        }
    }
}
