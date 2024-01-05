using Application.Common.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Projects.Queries.GetProjectAccessRights;

public sealed class GetProjectAccessRightsQueryHandler :
    IRequestHandler<GetProjectAccessRightsQuery, List<UserAccessRightsResponse>>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IMapper _mapper;

    public GetProjectAccessRightsQueryHandler(IProjectRepository projectRepository, IMapper mapper)
    {
        _projectRepository = projectRepository;
        _mapper = mapper;
    }

    public async Task<List<UserAccessRightsResponse>> Handle(GetProjectAccessRightsQuery request, CancellationToken cancellationToken)
    {
        var projectAccessRights =
            await _projectRepository.GetProjectAccessRightsAsync(request.ProjectId, cancellationToken);
        return _mapper.Map<List<UserAccessRightsResponse>>(projectAccessRights);
    }
} 
