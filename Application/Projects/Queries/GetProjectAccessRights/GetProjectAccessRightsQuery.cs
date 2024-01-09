using MediatR;

namespace Application.Projects.Queries.GetProjectAccessRights;

public record GetProjectAccessRightsQuery : IRequest<List<UserAccessRightsResponse>>
{
    public Guid ProjectId { get; set; }
}
