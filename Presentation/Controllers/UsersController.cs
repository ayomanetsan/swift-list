using Application.Users.Commands.AcceptFriendRequest;
using Application.Users.Commands.SendFriendRequest;
using Application.Users.Queries.GetFriends;
using Application.Users.Queries.GetUsers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetUsersQuery(), cancellationToken);
        return Ok(response);
    }
    
    [HttpPost("send-friend-request")]
    public async Task<IActionResult> SendFriendRequest(SendFriendRequestCommand request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }
    
    [HttpPost("handle-friend-request")]
    public async Task<IActionResult> HandleFriendRequest(HandleFriendRequestCommand request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [HttpGet("friends")]
    public async Task<IActionResult> GetFriends(CancellationToken cancellationToken)
    {
        var userClaims = User.Claims;
        var email = userClaims.FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value;

        var response = await _mediator.Send(new GetFriendsQuery() { Email = email}, cancellationToken);
        return Ok(response);
    }
}
