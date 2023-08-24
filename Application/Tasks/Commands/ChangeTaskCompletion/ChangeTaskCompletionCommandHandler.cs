    using Application.Common.Interfaces;
    using MediatR;

    namespace Application.Tasks.Commands.ChangeTaskCompletion
    {
        public sealed class ChangeTaskCompletionCommandHandler : IRequestHandler<ChangeTaskCompletionCommand, Guid>
        {
            private ITaskRepository _taskRepository;
            private IUnitOfWork _unitOfWork;

            public ChangeTaskCompletionCommandHandler(ITaskRepository taskRepository, IUnitOfWork unitOfWork)
            {
                _taskRepository = taskRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Guid> Handle(ChangeTaskCompletionCommand request, CancellationToken cancellationToken)
            {
                await _taskRepository.ChangeCompletionAsync(request.Id, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return request.Id;
            }
        }
    }
