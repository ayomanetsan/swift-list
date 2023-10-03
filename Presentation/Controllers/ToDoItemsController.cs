using Application.ToDoItems.Commands.ChangeToDoItemCompletion;
using Application.ToDoItems.Commands.CreateToDoItem;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoItemsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ToDoItemsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> CreateToDoItem([FromBody] CreateToDoItemCommand request, CancellationToken cancellationToken)
        {
            var userClaims = User.Claims;
            var email = userClaims.FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")!.Value;
            request.CreatedBy = email;

            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [Authorize]
        [HttpPost("mark")]
        public async Task<ActionResult> ChangeCompletion([FromQuery] Guid id, CancellationToken cancellationToken)
        {
            var request = new ChangeToDoItemCompletionCommand() { ToDoItemId = id };
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }
    }
}
