using MediatR;

namespace Application.Projects.Queries.GetProjectsWithoutTasks
{
    public record GetProjectsWithoutTasksQuery : IRequest<IEnumerable<ProjectBriefResponse>>
    {
        public required string Email { get; set; }
    }
}
