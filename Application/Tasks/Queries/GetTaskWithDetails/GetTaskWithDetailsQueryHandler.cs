using Application.Common.Interfaces;
using Application.Tasks.Queries.GetTaskWithToDoItems;
using AutoMapper;
using MediatR;

namespace Application.Tasks.Queries.GetTaskWithDetails
{
    public sealed class GetTaskWithDetailsQueryHandler : IRequestHandler<GetTaskWithDetailsQuery, TaskWithDetailsResponse>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;

        public GetTaskWithDetailsQueryHandler(ITaskRepository taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }
   
        public async Task<TaskWithDetailsResponse> Handle(GetTaskWithDetailsQuery request, CancellationToken cancellationToken)
        {
            Domain.Entities.Task task = await _taskRepository.GetTaskWithDetailsAsync(request.TaskId, cancellationToken);
            return _mapper.Map<TaskWithDetailsResponse>(task);
        }
    }
}
