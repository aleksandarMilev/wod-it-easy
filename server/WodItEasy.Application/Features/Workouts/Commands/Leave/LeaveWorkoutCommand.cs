namespace WodItEasy.Application.Features.Workouts.Commands.Leave
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Common;

    public class LeaveWorkoutCommand : IRequest<Result>
    {
        private const string NotFoundErrorMessage = "Workout with Id: {0} found!";

        public int WorkoutId { get; set; }

        public int AthleteId { get; set; }

        public class LeaveWorkoutCommandHandler : IRequestHandler<LeaveWorkoutCommand, Result>
        {
            private readonly IWorkoutRepository repository;

            public LeaveWorkoutCommandHandler(IWorkoutRepository repository)
                => this.repository = repository;

            public async Task<Result> Handle(LeaveWorkoutCommand request, CancellationToken cancellationToken)
            {
                var workout = await this.repository.ByIdWithParticipants(request.WorkoutId, cancellationToken);

                if (workout is null)
                {
                    return string.Format(NotFoundErrorMessage, request.WorkoutId);
                }

                workout.RemoveParticipant(request.AthleteId);

                await this.repository.SaveAsync(workout, cancellationToken);

                return Result.Success;
            }
        }
    }
}
