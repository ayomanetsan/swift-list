using Application.Common.Interfaces;
using Domain.Enums;
using MediatR;

namespace Application.Projects.Commands.GrantAccessRights;

public sealed class GrantAccessRightsCommandHandler : IRequestHandler<GrantAccessRightsCommand, AccessRights>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IUnitOfWork _unitOfWork;

    public GrantAccessRightsCommandHandler(IProjectRepository projectRepository, IUnitOfWork unitOfWork)
    {
        _projectRepository = projectRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<AccessRights> Handle(GrantAccessRightsCommand request, CancellationToken cancellationToken)
    {
        var accessRights = await _projectRepository.GrantAccessRightsAsync(request.ProjectId, request.Email,
            request.AccessRights, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return accessRights;
    }
} 
