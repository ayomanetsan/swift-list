using Domain.Common;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class ToDoItem : BaseAuditableEntity
    {
        public required string Title { get; set; }

        public bool IsCompleted { get; set; } = false;

        [JsonIgnore]
        public Task Task { get; set; } = null!;

        public Guid TaskId { get; set; }
    }
}
