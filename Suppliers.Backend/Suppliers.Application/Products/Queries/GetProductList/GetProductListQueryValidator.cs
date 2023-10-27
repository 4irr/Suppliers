using FluentValidation;

namespace Suppliers.Application.Products.Queries.GetProductList
{
    public class GetProductListQueryValidator : AbstractValidator<GetProductListQuery>
    {
        public GetProductListQueryValidator()
        {
            RuleFor(x => x.UserId).NotEqual(Guid.Empty);
        }
    }
}
