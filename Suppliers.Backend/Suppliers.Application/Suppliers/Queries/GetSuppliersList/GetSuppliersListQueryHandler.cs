using MediatR;
using Suppliers.Application.Interfaces;

namespace Suppliers.Application.Suppliers.Queries.GetSuppliersList
{
    public class GetSuppliersListQueryHandler : IRequestHandler<GetSuppliersListQuery, SuppliersListVm>
    {
        private readonly IUsersHttpClient _apiClient;

        public GetSuppliersListQueryHandler(IUsersHttpClient apiClient) => _apiClient = apiClient;

        public async Task<SuppliersListVm> Handle(GetSuppliersListQuery request, CancellationToken cancellationToken)
        {
            var users = await _apiClient.GetAllUsersAsync();
            var result = users?.Where(u => u.Role == "Supplier").ToList();

            return new SuppliersListVm { Suppliers = result };
        }
    }
}
