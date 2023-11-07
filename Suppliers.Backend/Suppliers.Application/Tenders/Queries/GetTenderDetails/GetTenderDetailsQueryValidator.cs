using FluentValidation;

namespace Suppliers.Application.Tenders.Queries.GetTenderDetails
{
    public class GetTenderDetailsQueryValidator : AbstractValidator<GetTenderDetailsQuery>
    {
        public GetTenderDetailsQueryValidator() 
        {
            RuleFor(query => query.TenderId)
                .NotEqual(Guid.Empty).WithMessage("Поле не должно быть пустым");
        }
    }
}
