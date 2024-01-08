using Domain.Enums;
using MediatR;

namespace Application.Users.Commands.AcceptFriendRequest;

public record HandleFriendRequestCommand : IRequest<FriendshipStatus>
{
    public required string UserEmail { get; set; }
    
    public required string RequesterEmail { get; set; }
    
    public FriendshipStatus FriendshipStatus { get; set; }
}
