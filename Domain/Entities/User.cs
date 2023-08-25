using Domain.Common;

namespace Domain.Entities
{
    public class User : BaseAuditableEntity
    {
        public string? Fullname { get; set; }

        public string? Email { get; set; }

        public string? PasswordHash { get; set; }

        public string? PasswordSalt { get; set; }
    }
}
