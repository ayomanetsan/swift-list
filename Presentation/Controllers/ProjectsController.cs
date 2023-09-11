using Application.Projects.Commands.CreateProject;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> CreateProject([FromBody] CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var userClaims = User.Claims;
            var email = userClaims.FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value;
            request.CreatedBy = email;

            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }
    }
}
