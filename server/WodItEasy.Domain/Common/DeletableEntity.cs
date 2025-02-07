namespace WodItEasy.Domain.Common
{
    using System;

    public class DeletableEntity<TId> : AuditableEntity<TId>, IDeletableEntity
        where TId : struct
    {
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public string? DeletedBy { get; set; }

        public void Restore()
            => this.IsDeleted = false;
    }
}
