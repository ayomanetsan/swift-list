using Application.Tasks.Queries.GetTaskWithDetails;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Tasks.Commands.UpdateTask
{
    public record UpdateTaskCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }

        public required string Title { get; set; }

        public required string Description { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset DueDate { get; set; }

        public required string CreatedBy { get; set; }

        public IEnumerable<LabelResponse> Labels { get; set; } = null!;

        public IEnumerable<ToDoItemResponse> ToDoItems { get; set; } = null!;
    }
}
