using AutoMapper;
using Shouldly;
using Suppliers.Application.Products.Queries.GetProductList;
using Suppliers.Persistence;
using Suppliers.Tests.Common;
using Xunit;

namespace Suppliers.Tests.Products.Queries
{
    [Collection("QueryCollection")]
    public class GetProductListQueryHandlerTests
    {
        private readonly SuppliersDbContext Context;
        private readonly IMapper Mapper;

        public GetProductListQueryHandlerTests(QueryTestFixture fixture)
        {
            Context = fixture.Context;
            Mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetProductDetailsQueryHandler_Success()
        {
            //Arrange
            var handler = new GetProductListQueryHandler(Context, Mapper);

            //Act
            var result = await handler.Handle(new GetProductListQuery
            {
                UserId = SuppliersDbContextFactory.UserAId
            },
            CancellationToken.None);

            //Assert
            result.ShouldBeOfType<ProductListVm>();
            result.Products?.Count.ShouldBe(2);
        }
    }
}
