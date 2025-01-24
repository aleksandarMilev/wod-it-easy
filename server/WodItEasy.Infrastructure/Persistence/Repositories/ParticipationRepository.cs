namespace WodItEasy.Infrastructure.Persistence.Repositories
{
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Features.Participations;
    using Domain.Models.Participation;
    using Microsoft.EntityFrameworkCore;

    internal class ParticipationRepository : DataRepository<Participation>, IParticipationRepository
    {
        public ParticipationRepository(WodItEasyDbContext data)
            : base(data) { }

        public async Task<bool> Delete(
            int athleteId,
            int workoutId,
            CancellationToken cancellationToken)
        {
            var participation = await this
                .All()
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    p => p.AthleteId == athleteId && p.WorkoutId == workoutId,
                    cancellationToken);

            if (participation is null)
            {
                return false;
            }

            this.Data.Remove(participation);
            await this.Data.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
