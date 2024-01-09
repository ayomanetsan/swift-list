using FluentValidation;

namespace Application.Projects.Queries.GetProjectWithArchivedTasks;
public class GetProjectWithArchivedTasksQueryValidator : AbstractValidator<GetProjectWithArchivedTasksQuery>
{
    public GetProjectWithArchivedTasksQueryValidator()
    {
        RuleFor(x => x.ProjectId)
                .NotEmpty().WithMessage("Project id is required.");
    }
}

