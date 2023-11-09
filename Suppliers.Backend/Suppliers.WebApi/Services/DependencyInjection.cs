using Microsoft.Extensions.DependencyInjection.Extensions;
using Suppliers.Application.Interfaces;
using Suppliers.WebApi.Infrastructure;

namespace Suppliers.WebApi.Services
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddUsersHttpClientServices(this IServiceCollection services)
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<HttpClientAuthorizationDelegatingHandler>();
            services.AddHttpClient<IUsersHttpClient, UsersHttpClient>()
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

            return services;
        }
    }
}
