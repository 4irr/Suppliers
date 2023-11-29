using AutoMapper;
using Shouldly;
using Suppliers.Application.Common.Exceptions;
using Suppliers.Application.Products.Queries.GetProductDetails;
using Suppliers.Persistence;
using Suppliers.Tests.Common;
using Xunit;

namespace Suppliers.Tests.Products.Queries
{
    [Collection("QueryCollection")]
    public class GetProductDetailsQueryHandlerTests
    {
        private readonly SuppliersDbContext Context;
        private readonly IMapper Mapper;

        public GetProductDetailsQueryHandlerTests(QueryTestFixture fixture)
        {
            Context = fixture.Context;
            Mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetProductDetailsQueryHandler_Success()
        {
            // Arrange
            var handler = new GetProductDetailsQueryHandler(Context, Mapper);

            // Act
            var result = await handler.Handle(
                new GetProductDetailsQuery
                {
                    Id = Guid.Parse("909F7C29-891B-4BE1-8504-21F84F262084")
                },
                CancellationToken.None);

            // Assert
            result.ShouldBeOfType<ProductDetailsVm>();
            result.Name.ShouldBe("Name2");
        }

        [Fact]
        public async Task GetProductDetailsQueryHandler_FailOnWrongId()
        {
            // Arrange
            var handler = new GetProductDetailsQueryHandler(Context, Mapper);

            // Act
            var result = await handler.Handle(
                new GetProductDetailsQuery
                {
                    Id = Guid.Parse("909F7C29-891B-4BE1-8504-21F84F262084")
                },
                CancellationToken.None);

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new GetProductDetailsQuery
                    {
                        Id = Guid.NewGuid()
                    },
                    CancellationToken.None));
        }
    }
}
