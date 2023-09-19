using FluentValidation;

namespace Application.Projects.Queries.GetProjectsWithoutTasks
{
    public class GetProjectsWithoutTasksQueryValidator : AbstractValidator<GetProjectsWithoutTasksQuery>
    {
        public GetProjectsWithoutTasksQueryValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Provided email is not valid.");
        }
    }
}
