namespace WodItEasy.Infrastructure.Persistence.Configurations
{
    using Domain.Models.Athletes;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using static Domain.Models.ModelConstants.AthleteConstants;

    public class AthleteConfiguration : IEntityTypeConfiguration<Athlete>
    {
        public void Configure(EntityTypeBuilder<Athlete> builder)
        {
            builder
                .HasKey(a => a.Id);

            builder
                .Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(MaxNameLength);

            builder
                .Property(a => a.UserId)
                .IsRequired();

            builder
                .HasMany(a => a.Participations)
                .WithOne(p => p.Athlete)
                .HasForeignKey(p => p.AthleteId);

            builder
                .HasIndex(a => a.UserId);
        }
    }
}