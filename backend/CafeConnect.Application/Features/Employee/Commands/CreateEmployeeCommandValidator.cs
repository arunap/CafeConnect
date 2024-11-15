using FluentValidation;

namespace CafeConnect.Application.Features.Employee.Commands
{
    public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeCommandValidator()
        {
            // Name validation
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(6, 10).WithMessage("Name must be between 6 and 10 characters.");

            // EmailAddress validation
            RuleFor(x => x.EmailAddress)
                .NotEmpty().WithMessage("Email address is required.")
                .EmailAddress().WithMessage("A valid email address is required.");

            // PhoneNumber validation
            RuleFor(e => e.PhoneNumber)
                .InclusiveBetween(80000000, 99999999)
                .WithMessage("Phone number must start with 9 or 8 and contain exactly 8 digits.");

            // Gender validation
            RuleFor(x => x.Gender)
                .IsInEnum().WithMessage("Gender must be a valid value.");
        }
    }
}