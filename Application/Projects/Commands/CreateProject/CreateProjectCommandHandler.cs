using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Projects.Commands.CreateProject
{
    public sealed class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, Guid>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateProjectCommandHandler(IProjectRepository projectRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            Project project = _mapper.Map<Project>(request);
            _projectRepository.Create(project);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return project.Id;
        }
    }
}
