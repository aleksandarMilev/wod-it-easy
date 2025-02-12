namespace WodItEasy.Application.Features.Workouts.Queries.Details
{
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Common;
    using MediatR;

    public class WorkoutDetailsQuery : EntityQuery<int>, IRequest<WorkoutDetailsOutputModel?>
    {
        public class WorkoutDetailsQueryHandler : IRequestHandler<WorkoutDetailsQuery, WorkoutDetailsOutputModel?>
        {
            private readonly IWorkoutRepository repository;

            public WorkoutDetailsQueryHandler(IWorkoutRepository repository) 
                => this.repository = repository;

            public Task<WorkoutDetailsOutputModel?> Handle(WorkoutDetailsQuery request, CancellationToken cancellationToken) 
                => this.repository.Details(request.Id, cancellationToken);
        }
    }
}
