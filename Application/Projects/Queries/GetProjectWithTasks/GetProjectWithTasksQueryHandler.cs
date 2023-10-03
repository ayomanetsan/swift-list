using Application.Common.Interfaces;
using AutoMapper;
using Domain.Exceptions;
using MediatR;

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
            var project = await _projectRepository.GetProjectWithTasksAsync(request.ProjectId, cancellationToken);

            if (project is null)
            {
                throw new ProjectNotFoundException(request.ProjectId);
            }

            return _mapper.Map<ProjectResponse>(project);
        }
    }
}
