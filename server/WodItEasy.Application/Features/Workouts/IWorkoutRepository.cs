namespace WodItEasy.Application.Features.Workouts
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Common;
    using Application.Contracts;
    using Application.Features.Workouts.Queries.Details;
    using Application.Features.Workouts.Queries.Search;
    using Domain.Models.Workouts;

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
