using Domain.Common;

namespace Domain.Entities
{
    public class ToDoItem : BaseAuditableEntity
    {
        public required string Title { get; set; }

        public bool IsCompleted { get; set; } = false;

        public Task Task { get; set; } = null!;

        public Guid TaskId { get; set; }
    }
}
