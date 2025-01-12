namespace WodItEasy.Application.Features.Workouts
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Common;
    using Contracts;
    using Domain.Models.Workouts;
    using Queries.Details;
    using Queries.Search;
   
    public interface IWorkoutRepository : IRepository<Workout>
    {
        Task<Workout?> FindAsync(int id, CancellationToken cancellationToken = default);

        Task<PaginatedOutputModel<SearchWorkoutOutputModel>> PaginatedAsync(
            DateTime? startsAtDate,
            int pageSize,
            int pageIndex,
            CancellationToken cancellationToken = default);

        Task<WorkoutDetailsOutputModel?> DetailsAsync(int id, CancellationToken cancellationToken = default);

        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}
