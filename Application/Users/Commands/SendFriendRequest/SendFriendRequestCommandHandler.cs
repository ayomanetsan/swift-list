using Application.Common.Interfaces;
using AutoMapper;
using Domain.Enums;
using MediatR;

namespace Application.Users.Commands.SendFriendRequest;

public sealed class SendFriendRequestCommandHandler : IRequestHandler<SendFriendRequestCommand, FriendshipStatus>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SendFriendRequestCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<FriendshipStatus> Handle(SendFriendRequestCommand request, CancellationToken cancellationToken)
    {
        var friendshipStatus =
            await _userRepository.SendFriendRequestAsync(request.UserEmail, request.RequesterEmail, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return friendshipStatus;
    }
}
