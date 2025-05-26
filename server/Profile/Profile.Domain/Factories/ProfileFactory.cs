namespace WodItEasy.Profile.Domain.Factories
{
    using Models.Profile;

    public class ProfileFactory : IProfileFactory
    {
        private string userId = default!;
        private string displayName = default!;
        private string avatarUrl = default!;
        private string bio = default!;

        public IProfileFactory ForUser(string userId)
        {
            this.userId = userId;
            return this;
        }

        public IProfileFactory WithDisplayName(string displayName)
        {
            this.displayName = displayName;
            return this;
        }

        public IProfileFactory WithAvatarUrl(string avatarUrl)
        {
            this.avatarUrl = userId;
            return this;
        }

        public IProfileFactory WithBio(string bio)
        {
            this.bio = bio;
            return this;
        }

        public Profile Build()
            => new(
                this.userId,
                this.displayName,
                this.avatarUrl,
                this.bio);
    }
}
