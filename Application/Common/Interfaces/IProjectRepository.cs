using Domain.Entities;
using Domain.Enums;
using System.Linq.Expressions;
using Task = Domain.Entities.Task;

namespace Application.Common.Interfaces;

public interface IProjectRepository : IRepository<Project>
{
    Task<IEnumerable<Project>> GetProjectsWithoutTasksAsync(string email, CancellationToken cancellationToken);

    Task<Project> GetProjectWithTasksAsync(Guid projectId, Func<Task, bool> taskPredicate, CancellationToken cancellationToken);

    //Task<Project> GetProjectWithArchivedTasksAsync(Guid projectId, CancellationToken cancellationToken);

    Task<AccessRights> GetAccessRightsAsync(Guid projectId, string email, CancellationToken cancellationToken);

    Task<AccessRights> GrantAccessRightsAsync(Guid projectId, string email, AccessRights accessRights,
        CancellationToken cancellationToken);

    Task<IEnumerable<ProjectUsers>> GetProjectAccessRightsAsync(Guid projectId, CancellationToken cancellationToken);
}
