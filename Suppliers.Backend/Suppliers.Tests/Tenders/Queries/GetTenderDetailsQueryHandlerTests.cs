using AutoMapper;
using Shouldly;
using Suppliers.Application.Common.Exceptions;
using Suppliers.Application.Tenders.Queries.GetTenderDetails;
using Suppliers.Persistence;
using Suppliers.Tests.Common;
using Xunit;

namespace Suppliers.Tests.Tenders.Queries
{
    [Collection("QueryCollection")]
    public class GetTenderDetailsQueryHandlerTests
    {
        private readonly SuppliersDbContext Context;
        private readonly IMapper Mapper;

        public GetTenderDetailsQueryHandlerTests(QueryTestFixture fixture)
        {
            Context = fixture.Context;
            Mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetTenderDetailsQueryHandler_Success()
        {
            //Arrange
            var handler = new GetTenderDetailsQueryHandler(Context, Mapper);

            //Act
            var result = await handler.Handle(new GetTenderDetailsQuery
            {
                TenderId = Guid.Parse("4EEE8331-2CD6-4D29-BE0F-515C28A85A98")
            },
            CancellationToken.None);

            //Assert
            result.ShouldBeOfType<TenderDetailsVm>();
            result.Title.ShouldBe("Title4");
            result.Description.ShouldBe("Description4");
        }

        [Fact]
        public async Task GetTenderDetailsQueryHandler_FailOnWrongId()
        {
            //Arrange
            var handler = new GetTenderDetailsQueryHandler(Context, Mapper);

            //Act

            //Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(new GetTenderDetailsQuery
                {
                    TenderId = Guid.NewGuid()
                },
                CancellationToken.None));
        }
    }
}
