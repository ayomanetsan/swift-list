using Domain.Common;

namespace Domain.Entities
{
    public class Task : BaseAuditableEntity
    {
        public required string Title { get; set; }

        public required string Description { get; set; }

        public bool IsCompleted { get; set; } = false;
    }
}
