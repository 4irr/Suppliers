using Microsoft.EntityFrameworkCore;
using Suppliers.Application.Common.Exceptions;
using Suppliers.Application.Products.Commands.DeleteProduct;
using Suppliers.Tests.Common;
using Xunit;

namespace Suppliers.Tests.Products.Commands
{
    public class DeleteProductCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task DeleteProductCommandHandler_FailOnWrongId()
        {
            //Arrange
            var handler = new DeleteProductCommandHandler(Context);

            //Act

            //Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(new DeleteProductCommand
                {
                    UserId = SuppliersDbContextFactory.UserAId,
                    Id = Guid.NewGuid()
                },
                CancellationToken.None));
        }

        [Fact]
        public async Task DeleteProductCommandHandler_FailOnWrongUserId()
        {
            //Arrange
            var handler = new DeleteProductCommandHandler(Context);

            //Act

            //Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(new DeleteProductCommand
                {
                    UserId = SuppliersDbContextFactory.UserBId,
                    Id = SuppliersDbContextFactory.ProductIdForDelete
                },
                CancellationToken.None));
        }

        [Fact]
        public async Task DeleteProductCommandHandler_Success()
        {
            //Arrange
            var handler = new DeleteProductCommandHandler(Context);

            //Act
            await handler.Handle(new DeleteProductCommand
            {
                UserId = SuppliersDbContextFactory.UserAId,
                Id = SuppliersDbContextFactory.ProductIdForDelete
            },
            CancellationToken.None);

            //Assert
            Assert.Null(await Context.Products.SingleOrDefaultAsync(product =>
                product.Id == SuppliersDbContextFactory.ProductIdForDelete));
        }
    }
}
