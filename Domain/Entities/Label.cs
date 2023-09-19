using Domain.Common;

namespace Domain.Entities
{
    public class Label : BaseAuditableEntity
    {
        public required string Title { get; set; }

        public required string Color { get; set; }

        public Task Task { get; set; } = null!;

        public Guid TaskId { get; set; }
    }
}
