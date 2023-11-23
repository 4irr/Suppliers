using FluentValidation;

namespace Suppliers.Application.Users.Commands.UnlockUser
{
    public class UnlockUserCommandValidator : AbstractValidator<UnlockUserCommand>
    {
        public UnlockUserCommandValidator() 
        {
            RuleFor(command => command.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
