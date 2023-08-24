using MediatR;
using Task = Domain.Entities.Task;

namespace Application.Tasks.Queries.GetTasks
{
    public record GetTasksQuery : IRequest<List<Task>>
    {
        public required string Email { get; set; }
    }
}
