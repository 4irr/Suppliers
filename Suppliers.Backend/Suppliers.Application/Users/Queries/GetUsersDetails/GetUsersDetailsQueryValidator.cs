using FluentValidation;

namespace Suppliers.Application.Suppliers.Queries.GetSupplierDetails
{
    public class GetUsersDetailsQueryValidator : AbstractValidator<GetUsersDetailsQuery>
    {
        public GetUsersDetailsQueryValidator() 
        {
            RuleFor(supplierDetailsQuery => supplierDetailsQuery.Id)
                .NotEqual(Guid.Empty).WithMessage("Поле не должно быть пустым");
        }
    }
}
