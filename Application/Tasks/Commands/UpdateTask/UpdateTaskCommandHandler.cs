using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Task = Domain.Entities.Task;

namespace Application.Tasks.Commands.UpdateTask
{
    public sealed class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, Guid>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateTaskCommandHandler(ITaskRepository taskRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            Task task = _mapper.Map<Task>(request);
            await _taskRepository.UpdateTaskWithDetailsAsync(task, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return task.Id;
        }
    }
}
