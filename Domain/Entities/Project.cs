using Domain.Common;

namespace Domain.Entities
{
    public class Project : BaseAuditableEntity
    {
        public required string Title { get; set; }

        public required string Description { get; set; }

        public ICollection<Task> Tasks { get; private set; } = new List<Task>();
    }
}
