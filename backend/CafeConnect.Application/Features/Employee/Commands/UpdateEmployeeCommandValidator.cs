using FluentValidation;

namespace CafeConnect.Application.Features.Employee.Commands
{
    public class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
    {
        public UpdateEmployeeCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Employee ID is required.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

            RuleFor(x => x.EmailAddress)
                .NotEmpty().WithMessage("Email address is required.")
                .EmailAddress().WithMessage("A valid email address is required.");

            RuleFor(e => e.PhoneNumber)
                .InclusiveBetween(80000000, 99999999)
                .WithMessage("Phone number must start with 9 or 8 and contain exactly 8 digits.");

            RuleFor(x => x.Gender)
                .IsInEnum().WithMessage("Gender is required and must be valid.");
        }
    }
}