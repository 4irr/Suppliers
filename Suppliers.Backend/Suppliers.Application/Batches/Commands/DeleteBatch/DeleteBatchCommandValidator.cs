using FluentValidation;

namespace Suppliers.Application.Batches.Commands.DeleteBatch
{
    public class DeleteBatchCommandValidator : AbstractValidator<DeleteBatchCommand>
    {
        public DeleteBatchCommandValidator() 
        {
            RuleFor(command => command.BatchId)
                .NotEmpty().WithMessage("Поле не должно быть пустым");
        }
    }
}
