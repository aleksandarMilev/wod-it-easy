namespace WodItEasy.Application.Features.Workouts.Commands.Join
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Common;

    public class JoinWorkoutCommand : IRequest<Result>
    {
        private const string NotFoundErrorMessage = "Workout with Id: {0} found!";

        public int WorkoutId { get; set; }

        public int AthleteId { get; set; } 

        public class JoinWorkoutCommandHandler : IRequestHandler<JoinWorkoutCommand, Result>
        {
            private readonly IWorkoutRepository repository;

            public JoinWorkoutCommandHandler(IWorkoutRepository repository) 
                => this.repository = repository;

            public async Task<Result> Handle(JoinWorkoutCommand request, CancellationToken cancellationToken)
            {
                var workout = await this.repository.ById(request.WorkoutId, cancellationToken);

                if (workout is null)
                {
                    return string.Format(NotFoundErrorMessage, request.WorkoutId);
                }

                workout.AddParticipant(request.AthleteId);

                await this.repository.SaveAsync(workout, cancellationToken);

                return Result.Success;
            }
        }
    }
}
