using Microsoft.EntityFrameworkCore;
using Suppliers.Application.Common.Exceptions;
using Suppliers.Application.Tenders.Commands.DeleteTender;
using Suppliers.Tests.Common;
using Xunit;

namespace Suppliers.Tests.Tenders.Commands
{
    public class DeleteTenderCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task DeleteProductCommandHandler_FailOnWrongId()
        {
            //Arrange
            var handler = new DeleteTenderCommandHandler(Context);

            //Act

            //Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(new DeleteTenderCommand
                {
                    TenderId = Guid.NewGuid()
                },
                CancellationToken.None));
        }

        [Fact]
        public async Task DeleteProductCommandHandler_Success()
        {
            //Arrange
            var handler = new DeleteTenderCommandHandler(Context);

            //Act
            await handler.Handle(new DeleteTenderCommand
            {
                TenderId = SuppliersDbContextFactory.TenderIdForDelete
            },
            CancellationToken.None);

            //Assert
            Assert.Null(await Context.Tenders.SingleOrDefaultAsync(tender =>
                tender.Id == SuppliersDbContextFactory.TenderIdForDelete));
        }
    }
}
