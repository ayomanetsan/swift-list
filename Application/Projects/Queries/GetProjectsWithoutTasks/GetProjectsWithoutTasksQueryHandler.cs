using Application.Common.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Projects.Queries.GetProjectsWithoutTasks
{
    public sealed class GetProjectsWithoutTasksQueryHandler : IRequestHandler<GetProjectsWithoutTasksQuery, IEnumerable<ProjectBriefResponse>>
    {
        private IProjectRepository _projectRepository;
        private IMapper _mapper;

        public GetProjectsWithoutTasksQueryHandler(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProjectBriefResponse>> Handle(GetProjectsWithoutTasksQuery request, CancellationToken cancellationToken)
        {
            var projects = await _projectRepository.GetProjectsWithoutTasksAsync(request.Email, cancellationToken);

            return _mapper.Map<IEnumerable<ProjectBriefResponse>>(projects);
        }
    }
}
