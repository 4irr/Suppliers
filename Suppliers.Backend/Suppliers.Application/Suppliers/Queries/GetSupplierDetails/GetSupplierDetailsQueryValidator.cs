using FluentValidation;

namespace Suppliers.Application.Suppliers.Queries.GetSupplierDetails
{
    public class GetSupplierDetailsQueryValidator : AbstractValidator<GetSupplierDetailsQuery>
    {
        public GetSupplierDetailsQueryValidator() 
        {
            RuleFor(supplierDetailsQuery => supplierDetailsQuery.Id)
                .NotEqual(Guid.Empty).WithMessage("Поле не должно быть пустым");
        }
    }
}
