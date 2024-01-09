using Domain.Enums;
using MediatR;

namespace Application.Users.Commands.SendFriendRequest;

public record SendFriendRequestCommand : IRequest<FriendshipStatus>
{
    public required string UserEmail { get; set; }
    
    public required string RequesterEmail { get; set; }
}
