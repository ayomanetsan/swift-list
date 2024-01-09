using Application.Users.Queries.GetUsers;
using MediatR;

namespace Application.Users.Queries.GetFriends;

public record GetFriendsQuery : IRequest<List<FriendResponse>>
{
    public required string Email { get; set; }
}
