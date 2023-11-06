using FluentValidation;

namespace Suppliers.Application.Contracts.Commands.ConfirmContract
{
    public class ConfirmContractCommandValidator : AbstractValidator<ConfirmContractCommand>
    {
        public ConfirmContractCommandValidator() 
        {
            RuleFor(command => command.ContractId)
                .NotEmpty().WithMessage("Поле не должно быть пустым");
        }
    }
}
