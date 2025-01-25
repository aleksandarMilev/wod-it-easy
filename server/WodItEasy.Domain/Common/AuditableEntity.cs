namespace WodItEasy.Domain.Common
{
    using System;

    public abstract class AuditableEntity<TId> : Entity<TId>, IAuditableEntity
        where TId : struct
    {
        public string CreatedBy { get; set; } = null!;

        public DateTime CreatedOn { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
