using Application.Common.Interfaces;
using MediatR;

namespace Application.ToDoItems.Commands.ChangeToDoItemCompletion
{
    public sealed class ChangeToDoItemCompletionCommandHandler : IRequestHandler<ChangeToDoItemCompletionCommand, Guid>
    {
        private readonly IToDoItemRepository _toDoItemRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ChangeToDoItemCompletionCommandHandler(IToDoItemRepository toDoItemRepository, IUnitOfWork unitOfWork)
        {
            _toDoItemRepository = toDoItemRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(ChangeToDoItemCompletionCommand request, CancellationToken cancellationToken)
        {
            await _toDoItemRepository.ChangeCompletionAsync(request.ToDoItemId, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return request.ToDoItemId;
        }
    }
}
