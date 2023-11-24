using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Suppliers.Application.Common.Exceptions;
using Suppliers.Application.Interfaces;
using Suppliers.Application.Products.Queries.GetProductList;
using Suppliers.Application.Suppliers.Queries.GetSuppliersList;
using Suppliers.Application.Reports.Commands.CreateReport;

namespace Suppliers.Application.Users.Commands.CalculateUserActivity
{
    public class CalculateUserActivityCommandHandler : IRequestHandler<CalculateUserActivityCommand, UserActivityVm>
    {
        private readonly IUsersHttpClient _apiClient;
        private readonly ISuppliersDbContext _context;
        private readonly IMapper _mapper;

        public CalculateUserActivityCommandHandler(IUsersHttpClient apiClient, ISuppliersDbContext context, IMapper mapper)
        {
            _apiClient = apiClient;
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserActivityVm> Handle(CalculateUserActivityCommand request, CancellationToken cancellationToken)
        {
            var user = await _apiClient.GetUserByIdAsync(request.SupplierId);

            if(user == null)
            {
                throw new NotFoundException(nameof(AppUserDto), request.SupplierId);
            }

            var products = await _context.Products
                .Where(product => product.UserId == request.SupplierId)
                .ProjectTo<ProductLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var contracts = await _context.Contracts
                .Where(contract => contract.Order.SupplierId == request.SupplierId && contract.IsConfirmed &&
                    (contract.ConclusionDate.Date >= request.Beginning && contract.ConclusionDate.Date <= request.Ending))
                .ProjectTo<ContractLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var tenders = await _context.TendersUsers
                .Where(tender => tender.UserId == request.SupplierId)
                .ProjectTo<TenderUserLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new UserActivityVm
            {
                Supplier = user.Organization,
                Beginning = request.Beginning.ToLongDateString(),
                Ending = request.Ending.ToLongDateString(),
                Products = products,
                Contracts = contracts,
                Tenders = tenders
            };

            return vm;
        }
    }
}
