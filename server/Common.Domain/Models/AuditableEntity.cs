namespace WodItEasy.Common.Domain.Models
{
    public abstract class AuditableEntity<TId> : Entity<TId>, IAuditableEntity
        where TId : struct
    {
        public string? CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
