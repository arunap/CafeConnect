using FluentValidation;

namespace CafeConnect.Application.Features.Cafe.Commands
{
    public class CreateCafeCommandValidator : AbstractValidator<CreateCafeCommand>
    {
        public CreateCafeCommandValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Name is required.");

            RuleFor(c => c.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(256).WithMessage("Description cannot exceed 256 characters");

            RuleFor(c => c.Location)
                .NotEmpty().WithMessage("Location is required.");
        }
    }
}