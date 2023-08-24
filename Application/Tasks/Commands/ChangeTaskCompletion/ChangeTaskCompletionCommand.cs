using MediatR;

namespace Application.Tasks.Commands.ChangeTaskCompletion
{
    public record ChangeTaskCompletionCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
    }
}
