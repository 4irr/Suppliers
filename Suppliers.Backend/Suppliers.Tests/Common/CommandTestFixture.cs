using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Suppliers.Application.Common.Services;
using Suppliers.Application.Interfaces;
using Suppliers.Persistence;
using Suppliers.WebApi.Infrastructure;
using Xunit;

namespace Suppliers.Tests.Common
{
    public class CommandTestFixture : IDisposable
    {
        public SuppliersDbContext Context;
        public EmailService EmailService;
        public IConfiguration Configuration;
        public IUsersHttpClient ApiClient;

        public CommandTestFixture()
        {
            Context = SuppliersDbContextFactory.Create();
            EmailService = new EmailService();
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            ApiClient = new UsersHttpClient(
                new HttpClient(new HttpClientAuthorizationDelegatingHandler(new HttpContextAccessor())), Configuration);
        }

        public void Dispose()
        {
            SuppliersDbContextFactory.Destroy(Context);
        }
    }

    [CollectionDefinition("CommandCollection")]
    public class CommandCollection : ICollectionFixture<CommandTestFixture> { }
}
