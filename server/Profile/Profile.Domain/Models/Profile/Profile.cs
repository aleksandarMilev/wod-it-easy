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
            string displayName,
            string? avatarUrl,
            string? bio)
        {
            this.UserId = userId;
            this.DisplayName = displayName;
            this.AvatarUrl = avatarUrl;
            this.Bio = bio;
        }

        public string UserId { get; }

        public string DisplayName { get; private set; }

        public string? AvatarUrl { get; private set; }

        public string? Bio { get; private set; }

        public Profile UpdateDisplayName(string displayName)
        {
            this.ValidateDisplayName(displayName);
            this.DisplayName = displayName;

            return this;
        }

        public Profile UpdateAvatarUrl(string avatarUrl)
        {
            this.ValidateAvatarUrl(avatarUrl);
            this.AvatarUrl = avatarUrl;

            return this;
        }

        public Profile UpdateBio(string bio)
        {
            this.ValidateBio(bio);
            this.Bio = bio;

            return this;
        }

        private void ValidateDisplayName(string name)
            => Guard.ForStringLength<InvalidProfileException>(
                name,
                MinDisplayNameLength,
                MaxDisplayNameLength,
                nameof(this.DisplayName));

        private void ValidateAvatarUrl(string avatarUrl)
            => Guard.ForStringLength<InvalidProfileException>(
                avatarUrl,
                UrlMinLength,
                UrlMaxLength,
                nameof(this.DisplayName));

        private void ValidateBio(string bio)
            => Guard.ForStringLength<InvalidProfileException>(
                bio,
                MinBioLength,
                MaxBioLength,
                nameof(this.DisplayName));
    }
}
