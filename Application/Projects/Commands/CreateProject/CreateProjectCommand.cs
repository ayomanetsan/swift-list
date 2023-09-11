using MediatR;

namespace Application.Projects.Commands.CreateProject
{
    public record CreateProjectCommand : IRequest<Guid>
    {
        public required string Title { get; set; }

        public required string Description { get; set; }

        public string? CreatedBy { get; set; }
    }
}
