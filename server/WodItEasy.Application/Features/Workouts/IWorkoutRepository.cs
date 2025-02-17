namespace WodItEasy.Application.Features.Workouts
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Common;
    using Contracts;
    using Domain.Models.Workouts;
    using Queries.Details;
    using Queries.Search;
   
    public interface IWorkoutRepository : IRepository<Workout>
    {
        Task<bool> ExistsById(
            int id, 
            CancellationToken cancellationToken = default);

        Task<Workout?> ById(
            int id, 
            CancellationToken cancellationToken = default);

        Task<IEnumerable<Workout>> ByDate(
            DateTime date, 
            int? excludeId = null, 
            CancellationToken cancellationToken = default);

        Task<Workout?> ByIdWithParticipants(
            int id, 
            CancellationToken cancellationToken = default);

        Task<PaginatedOutputModel<SearchWorkoutOutputModel>> Paginated(
            DateTime? startsAt,
            int pageIndex,
            int pageSize,
            CancellationToken cancellationToken = default);

        Task<WorkoutDetailsOutputModel?> Details(
            int id, 
            CancellationToken cancellationToken = default);

        Task<bool> Delete(
            int id, 
            CancellationToken cancellationToken = default);
    }
}
