using MediatR;

namespace Application.ToDoItems.Commands.ChangeToDoItemCompletion
{
    public record ChangeToDoItemCompletionCommand : IRequest<Guid>
    {
        public Guid ToDoItemId { get; init; }
    }
}
