using Microsoft.EntityFrameworkCore;
using Suppliers.Application.Common.Exceptions;
using Suppliers.Application.Tenders.Commands.UpdateTender;
using Suppliers.Tests.Common;
using Xunit;

namespace Suppliers.Tests.Tenders.Commands
{
    public class UpdateTenderCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task UpdateTenderCommandHandler_Success()
        {
            //Arrange
            var handler = new UpdateTenderCommandHandler(Context);
            var title = "tender title";
            var description = "tender description";

            //Act
            await handler.Handle(new UpdateTenderCommand
            {
                Id = SuppliersDbContextFactory.TenderIdForUpdate,
                Title = title,
                Description = description
            },
            CancellationToken.None);

            //Assert
            Assert.NotNull(await Context.Tenders.SingleOrDefaultAsync(tender =>
                tender.Id == SuppliersDbContextFactory.TenderIdForUpdate && tender.Title == title &&
                tender.Description == description));
        }

        [Fact]
        public async Task UpdateTenderCommandHandler_FailOnWrongId()
        {
            //Arrange
            var handler = new UpdateTenderCommandHandler(Context);
            var title = "tender title";
            var description = "tender description";

            //Act

            //Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(new UpdateTenderCommand
                {
                    Id = Guid.NewGuid(),
                    Title = title,
                    Description = description
                },
                CancellationToken.None));
        }
    }
}
