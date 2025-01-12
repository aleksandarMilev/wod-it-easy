namespace WodItEasy.Infrastructure.Persistence.Configurations
{
    using Domain.Models.Workouts;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class WorkoutConfiguration : IEntityTypeConfiguration<Workout>
    {
        private const int NameMaxLength = 100;
        private const int DescriptionMaxLength = 500;

        public void Configure(EntityTypeBuilder<Workout> builder)
        {
            builder
                .Property(w => w.Name)
                .IsRequired()
                .HasMaxLength(NameMaxLength);

            builder
                .Property(w => w.Description)
                .IsRequired()
                .HasMaxLength(DescriptionMaxLength);

            builder
                .Property(w => w.MaxParticipantsCount)
                .IsRequired();

            builder
                .Property(w => w.StartsAtDate)
                .IsRequired();

            builder
                .Property(w => w.StartsAtTime)
                .IsRequired();

            builder
                .OwnsOne(b => b.Type, t =>
                {
                    t.WithOwner();

                    t.Property(type => type.Value).IsRequired();
                });


            builder
                .HasMany(w => w.Participations)
                .WithOne(p => p.Workout)
                .HasForeignKey(p => p.WorkoutId);
        }
    }
}
