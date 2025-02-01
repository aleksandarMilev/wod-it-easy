namespace WodItEasy.Infrastructure.Persistence.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Common;
    using Application.Features.Workouts;
    using Application.Features.Workouts.Queries.Details;
    using Application.Features.Workouts.Queries.Search;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Domain.Models.Participation;
    using Domain.Models.Workouts;
    using Microsoft.EntityFrameworkCore;

    internal class WorkoutRepository : DataRepository<Workout>, IWorkoutRepository
    {
        private readonly IMapper mapper;

        public WorkoutRepository(WodItEasyDbContext data, IMapper mapper)
            : base(data) 
                => this.mapper = mapper;

        public async Task<bool> ExistsById(int id, CancellationToken cancellationToken = default)
            => await this
                .AllUpcomings()
                .AsNoTracking()
                .AnyAsync(w => w.Id == id, cancellationToken);

        public async Task<PaginatedOutputModel<SearchWorkoutOutputModel>> Paginated(
            string? startsAtDate,
            int pageIndex,
            int pageSize,
            CancellationToken cancellationToken = default)
        {
            var query = this
                .AllUpcomings()
                .AsNoTracking()
                .Where(w => startsAtDate == null
                        ? true
                        : DateTime.Parse(startsAtDate).Date == w.StartsAtDate.Date)
                .OrderBy(w => w.StartsAtDate.Date)
                .ThenByDescending(w => w.StartsAtTime)
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

        public async Task<WorkoutDetailsOutputModel?> Details(int id, CancellationToken cancellationToken = default)
            => await this
                .AllUpcomings()
                .AsNoTracking()
                .ProjectTo<WorkoutDetailsOutputModel>(this.mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(w => w.Id == id, cancellationToken);

        public async Task<Workout?> ById(int id, CancellationToken cancellationToken = default)
            => await this
                .AllUpcomings()
                .AsNoTracking()
                .FirstOrDefaultAsync(w => w.Id == id, cancellationToken);

        public async Task<IEnumerable<Workout>> ByDate(
            DateTime date,
            int? excludeId = null,
            CancellationToken cancellationToken = default)
                => await this
                    .AllUpcomings()
                    .AsNoTracking()
                    .Where(w => 
                        w.StartsAtDate.Date == date && 
                        w.Id != excludeId.GetValueOrDefault())
                    .ToListAsync(cancellationToken);

        public async Task<Workout?> ByIdWithParticipants(int id, CancellationToken cancellationToken = default)
            => await this
                .AllUpcomings()
                .AsNoTracking()
                .Include(w => w.Participations
                    .Where(p => p.Status == ParticipationStatus.Joined))
                .FirstOrDefaultAsync(w => w.Id == id, cancellationToken);

        public async Task<bool> Delete(int id, CancellationToken cancellationToken = default)
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

        private IQueryable<Workout> AllUpcomings()
            => this
                .All()
                .Where(w => w.StartsAtDate.Date >= DateTime.Now.Date);
    }
}
