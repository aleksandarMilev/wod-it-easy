namespace WodItEasy.Domain.Common
{
    using System;

    public interface IDeletableEntity : IAuditableEntity
    {
        bool IsDeleted { get; set; }

        DateTime? DeletedOn { get; set; }

        string? DeletedBy { get; set; }

        void Restore();
    }
}
