using FluentValidation;

namespace CafeConnect.Application.Features.Employee.Commands
{
    public class DeleteEmployeeCommandValidator : AbstractValidator<DeleteEmployeeCommand>
    {
        public DeleteEmployeeCommandValidator()
        {
            RuleFor(x => x.Id)
              .NotEmpty().WithMessage("Id is required.");
        }
    }
}