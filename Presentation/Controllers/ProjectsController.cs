using Application.Projects.Commands.CreateProject;
using Application.Projects.Commands.GrantAccessRights;
using Application.Projects.Queries.GetProjectAccessRights;
using Application.Projects.Queries.GetProjectsWithoutTasks;
using Application.Projects.Queries.GetProjectWithArchivedTasks;
using Application.Projects.Queries.GetProjectWithTasks;
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
            var email = userClaims.FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")!.Value;
            request.CreatedBy = email;

            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [Authorize]
        [HttpGet("get")]
        public async Task<IActionResult> GetProjects(CancellationToken cancellationToken)
        {
            var userClaims = User.Claims;
            var email = userClaims.FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")!.Value;

            var response = await _mediator.Send(new GetProjectsWithoutTasksQuery { Email = email }, cancellationToken);
            return Ok(response);
        }

        [Authorize]
        [HttpGet("get-tasks/{projectId}")]
        public async Task<IActionResult> GetProjectWithTasks(Guid projectId, CancellationToken cancellationToken)
        {
            var userClaims = User.Claims;
            var email = userClaims.FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")!.Value;
            
            var response = await _mediator.Send(new GetProjectWithTasksQuery { ProjectId = projectId, Email = email }, cancellationToken);
            return Ok(response);
        }

        [Authorize]
        [HttpGet("get-archived-tasks/{projectId}")]
        public async Task<IActionResult> GetProjectWithArchivedTasks(Guid projectId, CancellationToken cancellationToken)
        {
            var userClaims = User.Claims;
            var email = userClaims.FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")!.Value;

            var response = await _mediator.Send(new GetProjectWithArchivedTasksQuery { ProjectId = projectId, Email = email }, cancellationToken);
            return Ok(response);
        }

        [Authorize]
        [HttpPost("grant-access-rights")]
        public async Task<IActionResult> GrantAccessRights(GrantAccessRightsCommand request,
            CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [Authorize]
        [HttpGet("{projectId}/access-rights")]
        public async Task<IActionResult> GetProjectAccessRights(Guid projectId, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetProjectAccessRightsQuery() { ProjectId = projectId },
                cancellationToken);
            return Ok(response);
        }
    }
}
