using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Task = Domain.Entities.Task;

namespace Application.Tasks.Commands.CreateTask
{
    public sealed class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, Guid>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateTaskCommandHandler(ITaskRepository taskRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            Task task = _mapper.Map<Task>(request);
            _taskRepository.Create(task);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return task.Id;
        }
    }
}
