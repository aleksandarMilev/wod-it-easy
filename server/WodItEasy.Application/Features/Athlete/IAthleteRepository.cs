namespace WodItEasy.Application.Features.Athlete
{
    using Domain.Models.Athletes;
    using Queries.Details;
    using WodItEasy.Common.Application.Contracts;

    public interface IAthleteRepository : IRepository<Athlete>
    {
        Task<GetAthleteDetailsOutputModel?> GetOutputModel(
            string userId, 
            CancellationToken cancellationToken = default);

        Task<int?> GetId(
            string userId,
            CancellationToken cancellationToken = default);

        Task<bool> ExistsById(
            int id, 
            CancellationToken cancellationToken = default);

        Task<Athlete?> ById(
            int id, 
            CancellationToken cancellationToken = default);

        Task<Athlete?> ByUserId(
            string userId, 
            CancellationToken cancellationToken = default);

        Task<Athlete?> GetDeleted(
            string userId, 
            CancellationToken cancellationToken = default);

        Task<bool> Delete(
            string userId, 
            CancellationToken cancellationToken = default);
    }
}
