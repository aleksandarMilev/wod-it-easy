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
    using Domain.Common;
    using Domain.Models.Workouts;
    using Microsoft.EntityFrameworkCore;

    internal class WorkoutRepository : DataRepository<Workout>, IWorkoutRepository
    {
        public WorkoutRepository(WodItEasyDbContext data)
            : base(data) {}

        public async Task<PaginatedOutputModel<SearchWorkoutOutputModel>> PaginatedAsync(
            DateTime? startsAtDate,
            int pageSize,
            int pageIndex,
            CancellationToken cancellationToken = default)
        {
            var workouts = await this
                .AllUpcomings()
                .AsNoTracking()
                .Where(w => startsAtDate.HasValue ? w.StartsAtDate == startsAtDate : true)
                .Select(w => new SearchWorkoutOutputModel()
                {
                    Id = w.Id,
                    Name = w.Name,
                    MaxParticipantsCount = w.MaxParticipantsCount,
                    CurrentParticipantsCount = w.CurrentParticipantsCount,
                    StartsAtDate = w.StartsAtDate,
                    StartsAtTime = w.StartsAtTime,
                    Type = Enumeration.NameFromValue<WorkoutType>(w.Type.Value)
                })
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new PaginatedOutputModel<SearchWorkoutOutputModel>(
                workouts,
                this.Total(),
                pageIndex,
                pageSize);
        }

        public async Task<WorkoutDetailsOutputModel?> DetailsAsync(int id, CancellationToken cancellationToken = default)
            => await this
                .AllUpcomings()
                .AsNoTracking()
                .Select(w => new WorkoutDetailsOutputModel()
                {
                    Id = w.Id,
                    Name = w.Name,
                    Description = w.Description,
                    MaxParticipantsCount = w.MaxParticipantsCount,
                    CurrentParticipantsCount = w.CurrentParticipantsCount,
                    StartsAtDate = w.StartsAtDate,
                    StartsAtTime = w.StartsAtTime,
                    Type = Enumeration.NameFromValue<WorkoutType>(w.Type.Value)
                })
                .FirstOrDefaultAsync(w => w.Id == id, cancellationToken);

        public async Task<Workout?> FindAsync(int id, CancellationToken cancellationToken = default)
            => await this
                .AllUpcomings()
                .FirstOrDefaultAsync(w => w.Id == id, cancellationToken);

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var workout = await this.FindAsync(id, cancellationToken);

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
