namespace WodItEasy.Application.Features.Participations
{
    using System.Threading;
    using System.Threading.Tasks;
    using Common;
    using Contracts;
    using Domain.Models.Participation;
    using Queries.Mine;

    public interface IParticipationRepository : IRepository<Participation>
    {
        Task<Participation?> ById(int id, CancellationToken cancellationToken = default);

        Task<bool> Exists(
            int athleteId,
            int workoutId,
            CancellationToken cancellationToken = default);

        Task<PaginatedOutputModel<MyParticipationsOutputModel>> Mine(
             int athleteId,
             int pageIndex,
             int pageSize,
             CancellationToken cancellationToken = default);

        Task<int> GetId(
            int athleteId,
            int workoutId,
            CancellationToken cancellationToken = default);

        Task<bool> Delete(int id, CancellationToken cancellationToken = default);
    }
}
