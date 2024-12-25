namespace WodItEasy.Infrastructure.Persistence.Configurations
{
    using System.Collections.Generic;
    using Domain.Models.Athletes;
    using Domain.Models.Workouts;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class AthleteConfiguration : IEntityTypeConfiguration<Athlete>
    {
        private const int NumberMaxLength = 20;
        private const int NameMaxLength = 20;
        private const int EmailMaxLength = 20;

        public void Configure(EntityTypeBuilder<Athlete> builder)
        {
            builder
                .HasKey(a => a.Id);

            builder
                .Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(NameMaxLength);

            builder
                .Property(a => a.Email)
                .IsRequired()
                .HasMaxLength(EmailMaxLength);

            builder
                .OwnsOne(
                    a => a.PhoneNumber,
                    ph => 
                    {
                        ph
                            .Property(p => p.Number)
                            .IsRequired()
                            .HasMaxLength(NumberMaxLength);
                    });

            builder
                .HasOne(a => a.Membership)
                .WithOne()
                .HasForeignKey<Membership>("AthleteId")
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .Ignore(a => a.Workouts);

            builder
                .HasMany<Workout>()
                .WithMany()
                .UsingEntity<Dictionary<string, object>>(
                    "AthleteWorkouts",
                    j => j
                        .HasOne<Workout>()
                        .WithMany()
                        .HasForeignKey("WorkoutId"),
                    j => j
                        .HasOne<Athlete>()
                        .WithMany()
                        .HasForeignKey("AthleteId"),
                    j =>
                    {
                        j.ToTable("AthletesWorkouts");
                        j.HasKey("AthleteId", "WorkoutId");
                    });
        }
    }
}
