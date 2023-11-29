using AutoMapper;
using Shouldly;
using Suppliers.Application.Tenders.Queries.GetTendersList;
using Suppliers.Persistence;
using Suppliers.Tests.Common;
using Xunit;

namespace Suppliers.Tests.Tenders.Queries
{
    [Collection("QueryCollection")]
    public class GetTenderListQueryHandlerTests
    {
        private readonly SuppliersDbContext Context;
        private readonly IMapper Mapper;

        public GetTenderListQueryHandlerTests(QueryTestFixture fixture)
        {
            Context = fixture.Context;
            Mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetTenderListQueryHandler_Success()
        {
            //Arrange
            var handler = new GetTenderListQueryHandler(Context, Mapper);

            //Act
            var result = await handler.Handle(new GetTenderListQuery(), CancellationToken.None);

            //Assert
            result.ShouldBeOfType<TendersListVm>();
            result.Tenders?.Count.ShouldBe(4);
        }
    }
}
