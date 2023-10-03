using MediatR;

namespace Application.ToDoItems.Commands.CreateToDoItem
{
    public record CreateToDoItemCommand : IRequest<Guid>
    {
        public required string Title { get; set; }

        public Guid TaskId { get; set; }

        public string? CreatedBy { get; set; }
    }
}
