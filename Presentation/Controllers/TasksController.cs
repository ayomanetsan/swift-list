using Application.Tasks.Commands.ChangeTaskCompletion;
using Application.Tasks.Commands.CreateTask;
using Application.Tasks.Queries.GetTasks;
using Application.Tasks.Queries.GetTaskWithToDoItems;
using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

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

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllTasks(CancellationToken cancellationToken)
        {
            var userClaims = User.Claims;
            var email = userClaims.FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")!.Value;
            var request = new GetTasksQuery() { Email = email };

            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [Authorize]
        [HttpGet("get/{taskId}")]
        public async Task<IActionResult> GetTaskWithDetails([FromRoute] Guid taskId, CancellationToken cancellationToken)
        {
            var request = new GetTaskWithDetailsQuery() { TaskId = taskId };
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }
    }
}
