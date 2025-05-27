namespace WodItEasy.Profile.Domain.Factories
{
    using Common.Domain;
    using Domain.Models.Profile;

    public interface IProfileFactory : IFactory<Profile>
    {
        IProfileFactory ForUser(string userId);

        IProfileFactory WithDisplayName(string displayName);

        IProfileFactory WithAvatarUrl(string? avatarUrl);

        IProfileFactory WithBio(string? bio);
    }
}
