using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Task = Domain.Entities.Task;

namespace Application.Tasks.Commands.CreateTask
{
    public sealed class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, Guid>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ITaskRepository _taskRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateTaskCommandHandler(
            IProjectRepository projectRepository, 
            ITaskRepository taskRepository, 
            IUnitOfWork unitOfWork, 
            IMapper mapper)
        {
            _projectRepository = projectRepository;
            _taskRepository = taskRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            Project? project = await _projectRepository.GetAsync(request.ProjectId, cancellationToken);

            if (project is null)
            {
                throw new ProjectNotFoundException(request.ProjectId);
            }

            Task task = _mapper.Map<Task>(request);
            _taskRepository.Create(task);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return task.Id;
        }
    }
}
