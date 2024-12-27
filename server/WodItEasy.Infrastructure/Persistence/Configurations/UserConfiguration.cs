namespace WodItEasy.Infrastructure.Persistence.Configurations
{
    using Infrastructure.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasOne(u => u.Athlete)
                .WithOne()
                .HasForeignKey<User>("AthleteId")
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
