namespace WodItEasy.Infrastructure.Persistence.Configurations
{
    using System.Collections.Generic;
    using Domain.Models.Athletes;
    using Domain.Models.Workouts;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class AthleteConfiguration : IEntityTypeConfiguration<Athlete>
    {
        private const int NameMaxLength = 20;

        public void Configure(EntityTypeBuilder<Athlete> builder)
        {
            builder
                .HasKey(a => a.Id);

            builder
                .Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(NameMaxLength);

            builder
                .HasOne(a => a.Membership)
                .WithOne()
                .HasForeignKey<Membership>("AthleteId")
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
