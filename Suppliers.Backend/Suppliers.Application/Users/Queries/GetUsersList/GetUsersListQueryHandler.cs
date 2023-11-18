using MediatR;
using Suppliers.Application.Interfaces;

namespace Suppliers.Application.Suppliers.Queries.GetSuppliersList
{
    public class GetUsersListQueryHandler : IRequestHandler<GetUsersListQuery, UsersListVm>
    {
        private readonly IUsersHttpClient _apiClient;

        public GetUsersListQueryHandler(IUsersHttpClient apiClient) => _apiClient = apiClient;

        public async Task<UsersListVm> Handle(GetUsersListQuery request, CancellationToken cancellationToken)
        {
            var users = await _apiClient.GetAllUsersAsync();

            return new UsersListVm { Users = users };
        }
    }
}
