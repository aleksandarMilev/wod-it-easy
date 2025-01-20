namespace WodItEasy.Infrastructure.Persistence.Repositories
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Common;
    using Application.Features.Workouts;
    using Application.Features.Workouts.Queries.Details;
    using Application.Features.Workouts.Queries.Search;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Domain.Models.Workouts;
    using Microsoft.EntityFrameworkCore;

    internal class WorkoutRepository : DataRepository<Workout>, IWorkoutRepository
    {
        private readonly IMapper mapper;

        public WorkoutRepository(WodItEasyDbContext data, IMapper mapper)
            : base(data) 
                => this.mapper = mapper;

        public async Task<PaginatedOutputModel<SearchWorkoutOutputModel>> Paginated(
            DateTime? startsAtDate,
            int pageSize,
            int pageIndex,
            CancellationToken cancellationToken = default)
        {
            var workouts = await this
                .AllUpcomings()
                .AsNoTracking()
                .Where(w => startsAtDate.HasValue ? w.StartsAtDate == startsAtDate : true)
                .ProjectTo<SearchWorkoutOutputModel>(this.mapper.ConfigurationProvider)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new PaginatedOutputModel<SearchWorkoutOutputModel>(
                workouts,
                this.Total(),
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

        public async Task<Workout?> ByIdWithParticipants(int id, CancellationToken cancellationToken = default)
            => await this
                .AllUpcomings()
                .AsNoTracking()
                .Include(w => w.Participations)
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

        private int Total() => this.AllUpcomings().Count();

        private IQueryable<Workout> AllUpcomings()
            => this
                .All()
                .Where(w => w.StartsAtDate > DateTime.Now);
    }
}
