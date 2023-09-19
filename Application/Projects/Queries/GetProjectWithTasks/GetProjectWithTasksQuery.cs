using MediatR;

namespace Application.Projects.Queries.GetProjectWithTasks
{
    public record GetProjectWithTasksQuery : IRequest<ProjectResponse>
    {
        public Guid ProjectId { get; set; }
    }
}
