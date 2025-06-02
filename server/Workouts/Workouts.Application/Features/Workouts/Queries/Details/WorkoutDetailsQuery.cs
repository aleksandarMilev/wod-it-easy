namespace WodItEasy.Workouts.Application.Features.Workouts.Queries.Details
{
    using MediatR;
    using WodItEasy.Common.Application.Queries;

    public class WorkoutDetailsQuery
        : EntityQuery<int>, IRequest<WorkoutDetailsOutputModel?>
    {
        public class WorkoutDetailsQueryHandler(
            IWorkoutRepository repository)
            : IRequestHandler<WorkoutDetailsQuery, WorkoutDetailsOutputModel?>
        {
            private readonly IWorkoutRepository repository = repository;

            public async Task<WorkoutDetailsOutputModel?> Handle(
                WorkoutDetailsQuery request,
                CancellationToken cancellationToken)
                => await this.repository.Details(request.Id, cancellationToken);
        }
    }
}
