namespace WodItEasy.Infrastructure.Persistence.Configurations
{
    using Domain.Models.Athletes;
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
                .HasMany(a => a.Participations)
                .WithOne(p => p.Athlete)
                .HasForeignKey(p => p.AthleteId);
        }
    }
}