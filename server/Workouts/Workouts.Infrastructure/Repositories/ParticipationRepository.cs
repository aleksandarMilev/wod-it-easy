namespace WodItEasy.Workouts.Infrastructure.Repositories
{
    using Application.Features.Participations;
    using Application.Features.Participations.Queries.Mine;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Common.Application.Models;
    using Common.Infrastructure;
    using Domain.Models.Participation;
    using Microsoft.EntityFrameworkCore;
    using Persistence;

    internal class ParticipationRepository(
        WorkoutDbContext data, IMapper mapper)
        : DataRepository<WorkoutDbContext, Participation>(data),
          IParticipationRepository
    {
        private readonly IMapper mapper = mapper;

        public async Task<Participation?> ById(
            int id,
            CancellationToken cancellationToken = default)
            => await this
                .All()
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

        public async Task<bool> Exists(
            int athleteId,
            int workoutId,
            CancellationToken cancellationToken = default)
                => await this
                    .All()
                    .AsNoTracking()
                    .AnyAsync(
                        p => p.AthleteId == athleteId && p.WorkoutId == workoutId,
                        cancellationToken);

        public async Task<int> GetId(
            int athleteId,
            int workoutId,
            CancellationToken cancellationToken = default)
                => await this
                    .All()
                    .AsNoTracking()
                    .Where(p => p.AthleteId == athleteId && p.WorkoutId == workoutId)
                    .Select(p => p.Id)
                    .FirstOrDefaultAsync(cancellationToken);

        public async Task<PaginatedOutputModel<MyParticipationsOutputModel>> Mine(
            int athleteId,
            int pageIndex,
            int pageSize,
            CancellationToken cancellationToken = default)
        {
            var query = this
                .All()
                .AsNoTracking()
                .Where(p => p.AthleteId == athleteId)
                .OrderByDescending(p => p.JoinedAt)
                .ProjectTo<MyParticipationsOutputModel>(mapper.ConfigurationProvider);

            var total = query.Count();

            var participations = await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new PaginatedOutputModel<MyParticipationsOutputModel>(
                participations,
                total,
                pageIndex,
                pageSize);
        }

        public async Task<bool> Delete(
            int id,
            CancellationToken cancellationToken = default)
        {
            var participation = await this
                .All()
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

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
