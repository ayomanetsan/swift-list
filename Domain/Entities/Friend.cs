using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public class Friend : BaseAuditableEntity
{
    public Guid UserId { get; set; }

    public User User { get; set; } = null!;
    
    public Guid RequesterId { get; set; }

    public User Requester { get; set; } = null!;
    
    public FriendshipStatus FriendshipStatus { get; set; }
}
