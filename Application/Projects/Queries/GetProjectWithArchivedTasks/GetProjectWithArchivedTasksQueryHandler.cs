using Application.Common.Interfaces;
using Application.Projects.Queries.GetProjectWithTasks;
using AutoMapper;
using MediatR;

namespace Application.Projects.Queries.GetProjectWithArchivedTasks;
public sealed class GetProjectWithArchivedTasksQueryHandler : IRequestHandler<GetProjectWithArchivedTasksQuery, ProjectResponse>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IMapper _mapper;

    public GetProjectWithArchivedTasksQueryHandler(IProjectRepository projectRepository, IMapper mapper)
    {
        _projectRepository = projectRepository;
        _mapper = mapper;
    }
    public async Task<ProjectResponse> Handle(GetProjectWithArchivedTasksQuery request, CancellationToken cancellationToken)
    {
        var accessRights =
                await _projectRepository.GetAccessRightsAsync(request.ProjectId, request.Email, cancellationToken);
        var project = await _projectRepository.GetProjectWithArchivedTasksAsync(request.ProjectId, cancellationToken);

        var projectResponse = _mapper.Map<ProjectResponse>(project);
        projectResponse.AccessRights = accessRights;
        return projectResponse;
    }
}

