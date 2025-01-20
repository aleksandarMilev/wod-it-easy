namespace WodItEasy.Application.Features.Workouts.Queries.Details
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;

    public class WorkoutDetailsQuery : IRequest<WorkoutDetailsOutputModel?>
    {
        public WorkoutDetailsQuery(int id)
            => this.Id = id;

        public int Id { get; }

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
