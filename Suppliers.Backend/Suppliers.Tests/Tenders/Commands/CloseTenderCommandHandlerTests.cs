using Shouldly;
using Suppliers.Application.Tenders.Commands.CloseTender;
using Suppliers.Tests.Common;
using Xunit;

namespace Suppliers.Tests.Tenders.Commands
{
    public class CloseTenderCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task CloseTenderCommandHandler_Success()
        {
            //Arrange
            var handler = new CloseTenderCommandHandler(Context);

            //Act
            await handler.Handle(new CloseTenderCommand
            {
                TenderId = Guid.Parse("4EEE8331-2CD6-4D29-BE0F-515C28A85A98"),
                ExecutorId = SuppliersDbContextFactory.UserAId
            },
            CancellationToken.None);

            //Assert
            Context.Tenders.FirstOrDefault(tender => tender.Id == Guid.Parse("4EEE8331-2CD6-4D29-BE0F-515C28A85A98"))?
                .IsOpen.ShouldBe(false);
        }
    }
}
