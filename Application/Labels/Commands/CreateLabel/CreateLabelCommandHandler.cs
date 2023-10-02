using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Labels.Commands.CreateLabel
{
    public sealed class CreateLabelCommandHandler : IRequestHandler<CreateLabelCommand, Guid>
    {
        private readonly ILabelRepository _labelRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateLabelCommandHandler(ILabelRepository labelRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _labelRepository = labelRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateLabelCommand request, CancellationToken cancellationToken)
        {
            Label label = _mapper.Map<Label>(request);
            _labelRepository.Create(label);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            return label.Id;
        }
    }
}
