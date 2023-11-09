using AutoMapper;
using MediatR;
using Suppliers.Application.Common.Exceptions;
using Suppliers.Application.Interfaces;
using Suppliers.Application.Suppliers.Queries.GetSuppliersList;

namespace Suppliers.Application.Suppliers.Queries.GetSupplierDetails
{
    public class GetSupplierDetailsQueryHandler : IRequestHandler<GetSupplierDetailsQuery, SupplierDetailsVm>
    {
        private readonly IMapper _mapper;
        private readonly IUsersHttpClient _apiClient;

        public GetSupplierDetailsQueryHandler(IMapper mapper, IUsersHttpClient apiClient) => 
            (_mapper, _apiClient) = (mapper, apiClient);

        public async Task<SupplierDetailsVm> Handle(GetSupplierDetailsQuery request, CancellationToken cancellationToken)
        {
            var user = await _apiClient.GetUserByIdAsync(request.Id);

            if (user == null)
            {
                throw new NotFoundException(nameof(AppUserDto), request.Id);
            }

            return _mapper.Map<SupplierDetailsVm>(user);
        }
    }
}
