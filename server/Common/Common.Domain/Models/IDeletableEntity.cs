namespace WodItEasy.Common.Domain.Models
{
    public interface IDeletableEntity : IAuditableEntity
    {
        bool IsDeleted { get; set; }

        DateTime? DeletedOn { get; set; }

        string? DeletedBy { get; set; }

        void Restore();
    }
}
