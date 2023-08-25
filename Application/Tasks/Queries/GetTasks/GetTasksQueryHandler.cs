using Application.Common.Interfaces;
using MediatR;
using Task = Domain.Entities.Task;

namespace Application.Tasks.Queries.GetTasks
{
    public sealed class GetTasksQueryHandler : IRequestHandler<GetTasksQuery, List<Task>>
    {
        private readonly ITaskRepository _taskRepository;

        public GetTasksQueryHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<List<Task>> Handle(GetTasksQuery request, CancellationToken cancellationToken)
        {
            return await _taskRepository.GetTasksByEmailAsync(request.Email, cancellationToken);
        }
    }
}
