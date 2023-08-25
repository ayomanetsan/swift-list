using Application.Users.Commands.Register;
using Application.Users.Queries.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(JsonSerializer.Serialize(response));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginQuery request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(JsonSerializer.Serialize(response));
        }
    }
}
