using Application.Tasks.Queries.GetTaskWithDetails;
using MediatR;

namespace Application.Tasks.Commands.CreateTask
{
    public record CreateTaskCommand() : IRequest<Guid>
    {
        public Guid Id { get; set; }

        public Guid ProjectId { get; set; }

        public required string Title { get; set; }

        public required string Description { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset DueDate { get; set; }

        public required string CreatedBy { get; set; }

        public IEnumerable<LabelResponse> Labels { get; set; } = null!;

        public IEnumerable<ToDoItemResponse> ToDoItems { get; set; } = null!;
    }
}
