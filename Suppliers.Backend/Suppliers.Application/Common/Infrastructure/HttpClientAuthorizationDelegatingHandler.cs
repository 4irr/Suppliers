using IdentityModel.Client;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Suppliers.WebApi.Infrastructure
{
    public class HttpClientAuthorizationDelegatingHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpClientAuthorizationDelegatingHandler(IHttpContextAccessor httpContextAccessor) : base(new HttpClientHandler()) 
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
                                                                 CancellationToken cancellationToken)
        {
            string? authorizationHeader = _httpContextAccessor.HttpContext?.Session.GetString("token");

            if (!string.IsNullOrEmpty(authorizationHeader))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authorizationHeader);

                return await base.SendAsync(request, cancellationToken);
            }

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

            string? token = tokenResponse.AccessToken;

            if (token != null)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                _httpContextAccessor.HttpContext?.Session.SetString("token", token);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
