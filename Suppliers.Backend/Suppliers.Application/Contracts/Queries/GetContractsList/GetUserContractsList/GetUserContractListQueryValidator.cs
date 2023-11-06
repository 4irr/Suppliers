using FluentValidation;

namespace Suppliers.Application.Contracts.Queries.GetContractsList.GetUserContractsList
{
    public class GetUserContractListQueryValidator : AbstractValidator<GetUserContractsListQuery>
    {
        public GetUserContractListQueryValidator() 
        {
            RuleFor(query => query.UserId)
                .NotEmpty().WithMessage("Поле не должно быть пустым");
        }
    }
}
