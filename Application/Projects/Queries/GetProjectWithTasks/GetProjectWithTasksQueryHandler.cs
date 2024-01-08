using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using System.Linq.Expressions;
using Task = Domain.Entities.Task;

namespace Application.Projects.Queries.GetProjectWithTasks
{
    public sealed class GetProjectWithTasksQueryHandler : IRequestHandler<GetProjectWithTasksQuery, ProjectResponse>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public GetProjectWithTasksQueryHandler(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public async Task<ProjectResponse> Handle(GetProjectWithTasksQuery request, CancellationToken cancellationToken)
        {
            var accessRights =
                await _projectRepository.GetAccessRightsAsync(request.ProjectId, request.Email, cancellationToken);
            var project = await _projectRepository.GetProjectWithTasksAsync(request.ProjectId, false, cancellationToken);

            var projectResponse = _mapper.Map<ProjectResponse>(project);
            projectResponse.AccessRights = accessRights;
            return projectResponse;
        }
    }
}
