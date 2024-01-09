using Domain.Enums;
using MediatR;

namespace Application.Projects.Commands.GrantAccessRights;

public record GrantAccessRightsCommand : IRequest<AccessRights>
{
    public Guid ProjectId { get; set; }
    
    public required string Email { get; set; }
    
    public AccessRights AccessRights { get; set; }
}
