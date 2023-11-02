using IdentityModel.Client;
using MediatR;
using System.Text.Json;

namespace Suppliers.Application.Suppliers.Queries.GetSuppliersList
{
    public class GetSuppliersListQueryHandler : IRequestHandler<GetSuppliersListQuery, SuppliersListVm>
    {
        public async Task<SuppliersListVm> Handle(GetSuppliersListQuery request, CancellationToken cancellationToken)
        {
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync("https://localhost:7073");
            if (disco.IsError)
            {
                throw new Exception(disco.Error);
            }

            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,

                ClientId = "WebApi",
                ClientSecret = "secret",
                Scope = "IdentityServerApi"
            });

            if (tokenResponse.IsError)
            {
                throw new Exception(tokenResponse.Error);
            }

            var apiClient = new HttpClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken!);

            var response = await apiClient.GetAsync("https://localhost:7073/localApi/users");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to load data");
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();

                var result = JsonSerializer.Deserialize<List<AppUserDto>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                result = result?.Where(u => u.Role == "Supplier").ToList();

                return new SuppliersListVm { Suppliers = result };
            }
        }
    }
}
