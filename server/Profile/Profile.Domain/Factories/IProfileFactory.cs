namespace WodItEasy.Profile.Domain.Factories
{
    public interface IProfileFactory
    {
        IProfileFactory WithDisplayName(string displayName);

        IProfileFactory WithAvatarUrl(string avatarUrl);

        IProfileFactory WithBio(string bio);

        IProfileFactory ForUser(string userId);
    }
}
