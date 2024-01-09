using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public class ProjectUsers : BaseAuditableEntity
{
    public Guid UserId { get; set; }
    
    public User User { get; set; } = null!;
    
    public Guid ProjectId { get; set; }

    public Project Project { get; set; } = null!;
    
    public AccessRights AccessRights { get; set; }
}
