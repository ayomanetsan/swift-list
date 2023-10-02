using MediatR;

namespace Application.Tasks.Queries.GetTaskWithToDoItems
{
    public record GetTaskWithDetailsQuery : IRequest<TaskWithDetailsResponse>
    {
        public Guid TaskId { get; init; }
    }
}
