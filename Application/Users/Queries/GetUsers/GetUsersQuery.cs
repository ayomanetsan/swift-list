using MediatR;

namespace Application.Users.Queries.GetUsers;

public record GetUsersQuery : IRequest<List<UserResponse>>
{
}
