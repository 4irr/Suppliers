using AutoMapper;
using IdentityModel.Client;
using MediatR;
using Suppliers.Application.Common.Exceptions;
using Suppliers.Application.Suppliers.Queries.GetSuppliersList;
using System.Text.Json;

namespace Suppliers.Application.Suppliers.Queries.GetSupplierDetails
{
    public class GetSupplierDetailsQueryHandler : IRequestHandler<GetSupplierDetailsQuery, SupplierDetailsVm>
    {
        private readonly IMapper _mapper;

        public GetSupplierDetailsQueryHandler(IMapper mapper) => _mapper = mapper;

        public async Task<SupplierDetailsVm> Handle(GetSupplierDetailsQuery request, CancellationToken cancellationToken)
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

                var supplier = result?.FirstOrDefault(user => user.Id == request.Id.ToString());

                if(supplier == null)
                {
                    throw new NotFoundException(nameof(AppUserDto), request.Id);
                }

                return _mapper.Map<SupplierDetailsVm>(supplier);
            }
        }
    }
}
