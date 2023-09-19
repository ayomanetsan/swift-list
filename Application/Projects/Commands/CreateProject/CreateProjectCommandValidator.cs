using FluentValidation;

namespace Application.Projects.Commands.CreateProject
{
    public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
    {
        public CreateProjectCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MinimumLength(4).WithMessage("Title must be at least 4 characters long.")
                .MaximumLength(32).WithMessage("Title must not exceed 32 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MinimumLength(8).WithMessage("Description must be at least 4 characters long.");
        }
    }
}
