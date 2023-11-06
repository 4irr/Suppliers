using FluentValidation;

namespace Suppliers.Application.Contracts.Commands.DeleteContract
{
    public class DeleteContractCommandValidator : AbstractValidator<DeleteContractCommand>
    {
        public DeleteContractCommandValidator() 
        {
            RuleFor(command => command.ContractId)
                .NotEmpty().WithMessage("Поле не должно быть пустым");
        }
    }
}
