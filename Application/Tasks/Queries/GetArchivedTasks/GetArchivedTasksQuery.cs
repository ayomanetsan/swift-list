using MediatR;
using Task = Domain.Entities.Task;

namespace Application.Tasks.Queries.GetArchivedTasks;
public record GetArchivedTasksQuery : IRequest<List<Task>>
{
    public required string Email { get; set; }
}