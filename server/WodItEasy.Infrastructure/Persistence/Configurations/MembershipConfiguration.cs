namespace WodItEasy.Infrastructure.Persistence.Configurations
{
    using Domain.Models.Athletes;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class MembershipConfiguration : IEntityTypeConfiguration<Membership>
    {
        public void Configure(EntityTypeBuilder<Membership> builder)
        {
            builder
                .HasKey(m => m.Id);

            builder
                .Property(m => m.WorkoutsCount)
                .IsRequired();

            builder
                .Property(m => m.WorkoutsLeft)
                .IsRequired();

            builder
               .Property(m => m.StartsAt)
               .IsRequired();

            builder
                .OwnsOne(b => b.Type, t =>
                {
                    t.WithOwner();

                    t.Property(type => type.Value).IsRequired();
                });
        }
    }
}
