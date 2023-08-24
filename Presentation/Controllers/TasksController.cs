using Application.Tasks.Commands.ChangeTaskCompletion;
using Application.Tasks.Commands.CreateTask;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TasksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var userClaims = User.Claims;
            var email = userClaims.FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value;
            request.CreatedBy = email;

            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [Authorize]
        [HttpPut("mark")]
        public async Task ChangeCompletion([FromQuery] Guid id, CancellationToken cancellationToken)
        {
            var request = new ChangeTaskCompletionCommand() { Id = id };
            await _mediator.Send(request, cancellationToken);
        }
    }
}
