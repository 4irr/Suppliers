using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Suppliers.Application.Common.Exceptions;
using Suppliers.Application.Common.Services;
using Suppliers.Application.Interfaces;
using Suppliers.Application.Products.Commands.UpdateProduct;
using Suppliers.Persistence;
using Suppliers.Tests.Common;
using Xunit;

namespace Suppliers.Tests.Products.Commands
{
    [Collection("CommandCollection")]
    public class UpdateProductCommandHandlerTests
    {
        private readonly SuppliersDbContext Context;
        private readonly EmailService EmailService;
        private readonly IConfiguration Configuration;
        private readonly IUsersHttpClient ApiClient;

        public UpdateProductCommandHandlerTests(CommandTestFixture fixture) 
        {
            Context = fixture.Context;
            EmailService = fixture.EmailService;
            Configuration = fixture.Configuration;
            ApiClient = fixture.ApiClient;
        }

        [Fact]
        public async Task UpdateProductCommandHandler_Success()
        {
            // Arrange
            var handler = new UpdateProductCommandHandler(Context, EmailService, Configuration, ApiClient);
            var updatedName = "new name";

            // Act
            await handler.Handle(new UpdateProductCommand
            {
                Id = SuppliersDbContextFactory.ProductIdForUpdate,
                UserId = SuppliersDbContextFactory.UserBId,
                Name = updatedName,
                Price = 4,
                Quantity = 4,
                ExpirationDate = DateTime.Today
            }, CancellationToken.None);

            // Assert
            Assert.NotNull(await Context.Products.SingleOrDefaultAsync(product =>
                product.Id == SuppliersDbContextFactory.ProductIdForUpdate &&
                product.Name == updatedName));
        }

        [Fact]
        public async Task UpdateProductCommandHandler_FailOnWrongId()
        {
            // Arrange
            var handler = new UpdateProductCommandHandler(Context, EmailService, Configuration, ApiClient);

            // Act

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new UpdateProductCommand
                    {
                        Id = Guid.NewGuid(),
                        UserId = SuppliersDbContextFactory.UserBId
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task UpdateProductCommandHandler_FailOnWrongUserId()
        {
            // Arrange
            var handler = new UpdateProductCommandHandler(Context, EmailService, Configuration, ApiClient);

            // Act

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new UpdateProductCommand
                    {
                        Id = SuppliersDbContextFactory.ProductIdForUpdate,
                        UserId = SuppliersDbContextFactory.UserAId
                    },
                    CancellationToken.None));
        }
    }
}
