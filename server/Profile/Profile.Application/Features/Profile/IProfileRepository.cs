namespace WodItEasy.Profile.Application.Features.Profile
{
    using Common.Application.Contracts;
    using Domain.Models.Profile;
    using Queries.Details;

    public interface IProfileRepository : IRepository<Profile>
    {
        Task<Profile?> ById(
            string userId,
            CancellationToken cancellationToken = default);

        Task<ProfileDetailsOutputModel?> Details(
            string userId,
            CancellationToken cancellationToken = default);

        Task<bool> Delete(
            string userId,
            CancellationToken cancellationToken = default);
    }
}
