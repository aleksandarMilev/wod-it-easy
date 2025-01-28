namespace WodItEasy.Application.Features.Athlete
{
    using System.Threading;
    using System.Threading.Tasks;
    using Contracts;
    using Domain.Models.Athletes;
    using Queries.Get;

    public interface IAthleteRepository : IRepository<Athlete>
    {
        Task<GetAthleteOutputModel?> GetOutputModel(string userId, CancellationToken cancellationToken = default);

        Task<Athlete?> ByUserId(string userId, CancellationToken cancellationToken = default);

        Task<bool> ExistsById(int id, CancellationToken cancellationToken = default);

        Task<int?> GetId(string userId, CancellationToken cancellationToken = default);

        Task<bool> Delete(string userId, CancellationToken cancellationToken = default);
    }
}
