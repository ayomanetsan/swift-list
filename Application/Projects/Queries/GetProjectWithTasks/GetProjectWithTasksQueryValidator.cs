using FluentValidation;

namespace Application.Projects.Queries.GetProjectWithTasks
{
    public class GetProjectWithTasksQueryValidator : AbstractValidator<GetProjectWithTasksQuery>
    {
        public GetProjectWithTasksQueryValidator()
        {
            RuleFor(x => x.ProjectId)
                .NotEmpty().WithMessage("Project id is required.");
        }
    }
}
