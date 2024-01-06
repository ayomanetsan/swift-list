using Application.Projects.Queries.GetProjectWithTasks;
using MediatR;

namespace Application.Projects.Queries.GetProjectWithArchivedTasks;
public record GetProjectWithArchivedTasksQuery : IRequest<ProjectResponse>
{
    public Guid ProjectId { get; set; }

    public required string Email { get; set; }
}
