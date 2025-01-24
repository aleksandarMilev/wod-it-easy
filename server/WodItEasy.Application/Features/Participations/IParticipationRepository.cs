namespace WodItEasy.Application.Features.Participations
{
    using System.Threading;
    using System.Threading.Tasks;
    using Contracts;
    using Domain.Models.Participation;

    public interface IParticipationRepository : IRepository<Participation>
    {
        Task<bool> Delete(
            int athleteId,
            int workoutId,
            CancellationToken cancellationToken);
    }
}
