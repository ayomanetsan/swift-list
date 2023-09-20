using FluentValidation;
using System.Text.RegularExpressions;

namespace Application.Labels.Commands.CreateLabel
{
    public class CreateLabelCommandValidator : AbstractValidator<CreateLabelCommand>
    {
        public CreateLabelCommandValidator()
        {
            RuleFor(x => x.Title)
                .MinimumLength(4).WithMessage("Title must be at least 4 characters long.")
                .MaximumLength(16).WithMessage("Title must not exceed 16 characters.");

            RuleFor(x => x.Color)
                .NotEmpty().WithMessage("Color is required.")
                .Must(BeAValidColor).WithMessage("Provided color is not a hexadecimal value.");
        }

        private bool BeAValidColor(string color)
        {
            var regex = new Regex("^#?([a-f0-9]{6}|[a-f0-9]{3})$");
            return regex.IsMatch(color);
        }
    }
}
