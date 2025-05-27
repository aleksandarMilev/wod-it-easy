namespace WodItEasy.Profile.Infrastructure.Configurations
{
    using Domain.Models.Profile;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using static Common.Domain.Constants;
    using static Domain.Models.ModelConstants.ProfileConstants;

    public class ProfileConfiguration : IEntityTypeConfiguration<Profile>
    {
        public void Configure(EntityTypeBuilder<Profile> builder)
        {
            builder
                .HasKey(p => p.Id);

            builder
                .Property(p => p.DisplayName)
                .IsRequired()
                .HasMaxLength(MaxDisplayNameLength);

            builder
                .Property(p => p.AvatarUrl)
                .HasMaxLength(UrlMaxLength);

            builder
                .Property(p => p.Bio)
                .HasMaxLength(MaxBioLength);

            builder
                .Property(p => p.UserId)
                .IsRequired();
        }
    }
}