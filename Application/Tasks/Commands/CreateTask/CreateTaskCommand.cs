using MediatR;

namespace Application.Tasks.Commands.CreateTask
{
    public record CreateTaskCommand() : IRequest<Guid>
    {
        public required string Title { get; set; }

        public required string Description { get; set; }

        public Guid ProjectId { get; set; }

        public string? CreatedBy { get; set; }
    }
}
