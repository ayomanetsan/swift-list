using Application.Tasks.Commands.ChangeTaskCompletion;
using Application.Tasks.Commands.CreateTask;
using MediatR;
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

        [HttpPost("create")]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpPut("mark/{id}")]
        public async Task ChangeCompletion(Guid id, CancellationToken cancellationToken)
        {
            var request = new ChangeTaskCompletionCommand() { Id = id };
            await _mediator.Send(request, cancellationToken);
        }
    }
}
