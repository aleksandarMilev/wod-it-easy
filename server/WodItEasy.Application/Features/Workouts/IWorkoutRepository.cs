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
        Task<Workout?> Find(int id, CancellationToken cancellationToken = default);

        Task<PaginatedOutputModel<SearchWorkoutOutputModel>> Paginated(
            DateTime? startsAtDate,
            int pageSize,
            int pageIndex,
            CancellationToken cancellationToken = default);

        Task<WorkoutDetailsOutputModel?> Details(int id, CancellationToken cancellationToken = default);

        Task<bool> Delete(int id, CancellationToken cancellationToken = default);
    }
}
