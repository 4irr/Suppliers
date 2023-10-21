using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suppliers.Application.Products.Queries.GetProductDetails
{
    public class GetProductDetailsQueryValidator : AbstractValidator<GetProductDetailsQuery>
    {
        public GetProductDetailsQueryValidator()
        {
            RuleFor(productDetailsQuery => productDetailsQuery.Id)
                .NotEqual(Guid.Empty).WithMessage("Поле не должно быть пустым");
        }
    }
}
