using FluentValidation;
using Suppliers.Application.Interfaces;

namespace Suppliers.Application.Batches.Commands.CreateBatch
{
    public class CreateBatchCommandValidator : AbstractValidator<CreateBatchCommand>
    {
        private readonly ISuppliersDbContext? _context;

        public CreateBatchCommandValidator(ISuppliersDbContext context) => _context = context;

        public CreateBatchCommandValidator() 
        {
            RuleFor(command => command.ProductId)
                .NotEmpty().WithMessage("Поле не должно быть пустым");
            RuleFor(command => command.Quantity)
                .NotEmpty().WithMessage("Поле не должно быть пустым")
                .GreaterThan(0).WithMessage("Значение должно быть неотрицательным")
                .Must(IsQuantityValid).WithMessage("Значение не дожно быть больше значения количества продукта");
        }

        private bool IsQuantityValid(CreateBatchCommand cmd, int quantity)
        {
            return (_context!.Products.FirstOrDefault(pr => pr.Id == cmd.ProductId)?.Quantity >= quantity) ? true : false;
        }
    }
}
