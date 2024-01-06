using MediatR;

namespace Application.Tasks.Commands.ChangeTaskArchivation;
public record ChangeTaskArchivationCommand : IRequest<Guid>
{
    public Guid Id { get; set; }
}

