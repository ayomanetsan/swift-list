using MediatR;

namespace Application.Labels.Commands.CreateLabel
{
    public record CreateLabelCommand : IRequest<Guid>
    {
        public Guid TaskId { get; init; }

        public required string Title { get; init; }

        public required string Color { get; init; }
    }
}
