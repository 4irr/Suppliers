using Shouldly;
using Suppliers.Application.Common.Exceptions;
using Suppliers.Application.Tenders.Commands.RegisterInTender;
using Suppliers.Tests.Common;
using Xunit;

namespace Suppliers.Tests.Tenders.Commands
{
    public class RegisterInTenderCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task RegisterInTenderCommandHandler_Success()
        {
            //Arrange
            var handler = new RegisterInTenderCommandHandler(Context);

            //Act
            await handler.Handle(new RegisterInTenderCommand
            {
                TenderId = SuppliersDbContextFactory.TenderIdForRegister,
                UserId = SuppliersDbContextFactory.UserBId,
                UserDescription = "user description"
            },
            CancellationToken.None);

            //Assert
            Context.Tenders.FirstOrDefault(tender => tender.Id == SuppliersDbContextFactory.TenderIdForRegister)?
                .tenderUsers?.FirstOrDefault()?.UserId.ShouldBe(SuppliersDbContextFactory.UserBId);
        }

        [Fact]
        public async Task RegisterInTenderCommandHandler_FailOnWrongId()
        {
            //Arrange
            var handler = new RegisterInTenderCommandHandler(Context);

            //Act

            //Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(new RegisterInTenderCommand
                {
                    TenderId = Guid.NewGuid(),
                    UserId = SuppliersDbContextFactory.UserBId,
                    UserDescription = "user description"
                },
                CancellationToken.None));
        }
    }
}
