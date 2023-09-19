namespace Domain.Common
{
    public abstract class BaseAuditableEntity
    {
        public Guid Id { get; set; }

        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;

        public string? CreatedBy { get; set; }
    }
}
