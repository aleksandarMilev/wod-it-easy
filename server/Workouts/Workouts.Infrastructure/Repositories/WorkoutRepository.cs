namespace WodItEasy.Workouts.Infrastructure.Repositories
{
    using Application.Features.Workouts;
    using Application.Features.Workouts.Queries.Details;
    using Application.Features.Workouts.Queries.Search;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Common.Application.Models;
    using Common.Infrastructure;
    using Domain.Models.Participation;
    using Domain.Models.Workouts;
    using Microsoft.EntityFrameworkCore;
    using Persistence;

    internal class WorkoutRepository(
        WorkoutDbContext data,
        IMapper mapper)
        : DataRepository<WorkoutDbContext, Workout>(data),
          IWorkoutRepository
    {
        private readonly IMapper mapper = mapper;

        public async Task<bool> ExistsById(
            int id,
            CancellationToken cancellationToken = default)
            => await this
                .All()
                .AsNoTracking()
                .AnyAsync(w => w.Id == id, cancellationToken);

        public async Task<PaginatedOutputModel<SearchWorkoutOutputModel>> Paginated(
            DateTime? startsAt,
            int pageIndex,
            int pageSize,
            CancellationToken cancellationToken = default)
        {
            var query = this
                .All()
                .AsNoTracking()
                .Where(w => startsAt == null
                        ? true
                        : startsAt.Value.Date == w.StartsAt.Date)
                .OrderBy(w => w.StartsAt)
                .ProjectTo<SearchWorkoutOutputModel>(this.mapper.ConfigurationProvider);

            var total = query.Count();

            var workouts = await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new PaginatedOutputModel<SearchWorkoutOutputModel>(
                workouts,
                total,
                pageIndex,
                pageSize);
        }

        public async Task<WorkoutDetailsOutputModel?> Details(
            int id,
            CancellationToken cancellationToken = default)
            => await 
                All()
                .AsNoTracking()
                .ProjectTo<WorkoutDetailsOutputModel>(this.mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(w => w.Id == id, cancellationToken);

        public async Task<Workout?> ById(
            int id,
            CancellationToken cancellationToken = default)
            => await 
                All()
                .AsNoTracking()
                .FirstOrDefaultAsync(w => w.Id == id, cancellationToken);

        public async Task<IEnumerable<Workout>> ByDate(
            DateTime date,
            int? excludeId = null,
            CancellationToken cancellationToken = default)
                => await 
                    All()
                    .AsNoTracking()
                    .Where(w =>
                        w.StartsAt.Date == date.Date &&
                        w.Id != excludeId.GetValueOrDefault())
                    .ToListAsync(cancellationToken);

        public async Task<Workout?> ByIdWithParticipants(
            int id,
            CancellationToken cancellationToken = default)
            => await 
                All()
                .AsNoTracking()
                .Include(w => w.Participations
                    .Where(p => p.Status == ParticipationStatus.Joined))
                .FirstOrDefaultAsync(w => w.Id == id, cancellationToken);

        public async Task<bool> Delete(
            int id,
            CancellationToken cancellationToken = default)
        {
            var workout = await this.ById(id, cancellationToken);

            if (workout is null)
            {
                return false;
            }

            this.Data.Remove(workout);
            await this.Data.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
