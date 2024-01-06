using Application.Common.Interfaces;
using Application.Tasks.Commands.ChangeTaskArchivation;
using MediatR;

namespace Application.Tasks.Commands.ChangeTaskArchivation;
public class ChangeTaskArchivationHandler : IRequestHandler<ChangeTaskArchivationCommand, Guid>
{
    private ITaskRepository _taskRepository;
    private IUnitOfWork _unitOfWork;

    public ChangeTaskArchivationHandler(ITaskRepository taskRepository, IUnitOfWork unitOfWork) 
    {
        _taskRepository = taskRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(ChangeTaskArchivationCommand request, CancellationToken cancellationToken)
    {
        await _taskRepository.ChangeArchivationAsync(request.Id, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return request.Id;
    }
}

