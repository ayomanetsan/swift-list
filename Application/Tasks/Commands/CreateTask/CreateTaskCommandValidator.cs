using FluentValidation;

namespace Application.Tasks.Commands.CreateTask
{
    public class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
    {
        public CreateTaskCommandValidator() 
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MinimumLength(4).WithMessage("Title must be at least 4 characters long.")
                .MaximumLength(32).WithMessage("Title must not exceed 32 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MinimumLength(8).WithMessage("Description must be at least 8 characters long.");
        }
    }
}
