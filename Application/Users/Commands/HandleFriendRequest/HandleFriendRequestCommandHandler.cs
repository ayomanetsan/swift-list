using Application.Common.Interfaces;
using Domain.Enums;
using MediatR;

namespace Application.Users.Commands.AcceptFriendRequest;

public sealed class HandleFriendRequestCommandHandler : IRequestHandler<HandleFriendRequestCommand, FriendshipStatus>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public HandleFriendRequestCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<FriendshipStatus> Handle(HandleFriendRequestCommand request, CancellationToken cancellationToken)
    {
        var friendshipStatus = await _userRepository.HandleFriendRequestAsync(request.UserEmail, request.RequesterEmail,
            request.FriendshipStatus, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return friendshipStatus;
    }
} 
