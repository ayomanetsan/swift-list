using Application.Common.Interfaces;
using Application.Users.Queries.GetUsers;
using AutoMapper;
using MediatR;

namespace Application.Users.Queries.GetFriends;

public sealed class GetFriendsQueryHandler : IRequestHandler<GetFriendsQuery, List<FriendResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetFriendsQueryHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<List<FriendResponse>> Handle(GetFriendsQuery request, CancellationToken cancellationToken)
    {
        var friends = await _userRepository.GetFriendsAsync(request.Email, cancellationToken);
        return _mapper.Map<List<FriendResponse>>(friends);
    }
} 
