using Domain.Common;

namespace Domain.Entities
{
    public class Task : BaseAuditableEntity
    {
        public required string Title { get; set; }

        public required string Description { get; set; }

        public bool IsCompleted { get; set; } = false;

        public bool IsImortant { get; set; } = false;

        public bool IsArchived { get; set; } = false;

        public DateTimeOffset DueDate { get; set; }

        public ICollection<Label> Labels { get; private set; } = new List<Label>();

        public ICollection<ToDoItem> ToDoItems { get; private set; } = new List<ToDoItem>();

        public Guid ProjectId { get; set; }

        public Project Project { get; set; } = null!;
    }
}
