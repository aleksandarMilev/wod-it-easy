namespace WodItEasy.Profile.Application.Features.Profile
{
    using Common.Application.Contracts;
    using Domain.Models.Profile;

    public interface IProfileRepository : IRepository<Profile>
    {
        Task<Profile?> ById(
            int id,
            CancellationToken cancellationToken = default);

        Task<bool> Delete(
            int id,
            CancellationToken cancellationToken = default);
    }
}
