namespace WodItEasy.Infrastructure.Persistence.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using WodItEasy.Domain.Models.Participation;

    public class ParticipationConfiguration : IEntityTypeConfiguration<Participation>
    {
        public void Configure(EntityTypeBuilder<Participation> builder)
        {
            builder
                .HasKey(p => p.Id);

            builder
                .Property(p => p.JoinedAt)
                .IsRequired();


            builder
               .OwnsOne(b => b.Status, t =>
               {
                   t.WithOwner();

                   t.Property(type => type.Value).IsRequired();
               });

            builder
                .HasOne(p => p.Athlete)
                .WithMany(a => a.Participations)
                .HasForeignKey(p => p.AthleteId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(p => p.Workout)
                .WithMany(w => w.Participations)
                .HasForeignKey(p => p.WorkoutId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
