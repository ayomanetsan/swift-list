using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.ToDoItems.Commands.CreateToDoItem
{
    public sealed class CreateToDoItemCommandHandler : IRequestHandler<CreateToDoItemCommand, Guid>
    {
        private readonly IToDoItemRepository _toDoItemRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateToDoItemCommandHandler(IToDoItemRepository toDoItemRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _toDoItemRepository = toDoItemRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateToDoItemCommand request, CancellationToken cancellationToken)
        {
            ToDoItem toDoItem = _mapper.Map<ToDoItem>(request);
            _toDoItemRepository.Create(toDoItem);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return toDoItem.Id;
        }
    }
}
