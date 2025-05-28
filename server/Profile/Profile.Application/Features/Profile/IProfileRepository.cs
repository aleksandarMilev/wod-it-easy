namespace WodItEasy.Profile.Application.Features.Profile
{
    using Common.Application.Contracts;
    using Domain.Models.Profile;
    using Queries.Details;

    public interface IProfileRepository : IRepository<Profile>
    {
        Task<Profile?> ById(
            int id,
            CancellationToken cancellationToken = default);

        Task<ProfileDetailsOutputModel?> Details(
           int id,
           CancellationToken cancellationToken = default);

        Task<bool> Delete(
            int id,
            CancellationToken cancellationToken = default);
    }
}
