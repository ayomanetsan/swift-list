using Application.Common.Interfaces;
using MediatR;
using Task = Domain.Entities.Task;


namespace Application.Tasks.Queries.GetArchivedTasks;
public sealed class GetArchivedTasksQueryHandler : IRequestHandler<GetArchivedTasksQuery, List<Task>>
{
    private readonly ITaskRepository _taskRepository;

    public GetArchivedTasksQueryHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<List<Task>> Handle(GetArchivedTasksQuery request, CancellationToken cancellationToken)
    {
        return await _taskRepository.GetArchivedTasksByEmailAsync(request.Email, cancellationToken);
    }
}

