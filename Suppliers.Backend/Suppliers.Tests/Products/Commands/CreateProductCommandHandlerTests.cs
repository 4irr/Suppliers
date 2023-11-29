using Microsoft.EntityFrameworkCore;
using Suppliers.Application.Products.Commands.CreateProduct;
using Suppliers.Tests.Common;
using Xunit;

namespace Suppliers.Tests.Products.Commands
{
    public class CreateProductCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task CreateProductCommandHandler_Success()
        {
            //Arrange
            var handler = new CreateProductCommandHandler(Context);
            var name = "product name";
            float price = 1;

            //Act
            var productId = await handler.Handle(
                new CreateProductCommand
                {
                    UserId = SuppliersDbContextFactory.UserAId,
                    Name = name,
                    Price = price
                },
                CancellationToken.None);

            //Assert
            Assert.NotNull(await Context.Products.SingleOrDefaultAsync(product =>
                product.Name == name && product.UserId == SuppliersDbContextFactory.UserAId
                && product.Price == price));
        }
    }
}
