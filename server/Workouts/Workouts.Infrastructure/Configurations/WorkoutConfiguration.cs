﻿namespace WodItEasy.Workouts.Infrastructure.Configurations
{
    using Domain.Models.Workouts;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using static Common.Domain.Constants;
    using static Domain.Models.ModelConstants.WorkoutConstants;

    public class WorkoutConfiguration : IEntityTypeConfiguration<Workout>
    {
        public void Configure(EntityTypeBuilder<Workout> builder)
        {
            builder
                .Property(w => w.Name)
                .IsRequired()
                .HasMaxLength(MaxNameLength);

            builder
                .Property(w => w.Description)
                .IsRequired()
                .HasMaxLength(MaxDescriptionLength);

            builder
                .Property(w => w.ImageUrl)
                .IsRequired()
                .HasMaxLength(UrlMaxLength);

            builder
                .Property(w => w.MaxParticipantsCount)
                .IsRequired();

            builder
                .Property(w => w.StartsAt)
                .IsRequired();

            builder
                .OwnsOne(b => b.Type, t =>
                {
                    t.WithOwner();

                    t.Property(type => type.Value);
                });

            builder
                .HasMany(w => w.Participations)
                .WithOne(p => p.Workout)
                .HasForeignKey(p => p.WorkoutId);

            builder
                .HasIndex(w => w.StartsAt);

            builder
                .HasIndex(w => new { w.StartsAt, w.Id });
        }
    }
}
