namespace WodItEasy.Profile.Domain.Models.Profile
{
    using Common.Domain.Models;
    using Exceptions;

    using static Common.Domain.Constants;
    using static ModelConstants.ProfileConstants;

    public class Profile : DeletableEntity<int>, IAggregateRoot
    {
        internal Profile(
            string userId,
            string? avatarUrl,
            string? bio)
        {
            this.ValidateAvatarUrl(avatarUrl);
            this.ValidateBio(bio);

            this.UserId = userId;
            this.AvatarUrl = avatarUrl;
            this.Bio = bio;
        }

        public string UserId { get; }

        public string? AvatarUrl { get; private set; }

        public string? Bio { get; private set; }

        public Profile UpdateAvatarUrl(string? avatarUrl)
        {
            this.ValidateAvatarUrl(avatarUrl);
            this.AvatarUrl = avatarUrl;

            return this;
        }

        public Profile UpdateBio(string? bio)
        {
            this.ValidateBio(bio);
            this.Bio = bio;

            return this;
        }

        private void ValidateAvatarUrl(string? avatarUrl)
        {
            if (avatarUrl is not null)
            {
                Guard.ForStringLength<InvalidProfileException>(
                    avatarUrl,
                    UrlMinLength,
                    UrlMaxLength,
                    nameof(this.AvatarUrl));
            }
        }

        private void ValidateBio(string? bio)
        {
            if (bio is not null)
            {
                Guard.ForStringLength<InvalidProfileException>(
                    bio,
                    MinBioLength,
                    MaxBioLength,
                    nameof(this.Bio));
            }
        }
    }
}
