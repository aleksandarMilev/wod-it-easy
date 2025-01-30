namespace WodItEasy.Application.Features.Participations.Commands.Delete
{
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Features.Workouts;
    using Common;
    using MediatR;

    public class DeleteParticipationCommand : IRequest<Result>
    {
        public DeleteParticipationCommand(int athleteId, int workoutId)
        {
            this.AthleteId = athleteId;
            this.WorkoutId = workoutId;
        }

        public int AthleteId { get; set; }

        public int WorkoutId { get; set; }

        public class DeleteParticipationCommandHandler : IRequestHandler<DeleteParticipationCommand, Result>
        {
            private const string NotFoundErrorMessage = "Athlete with Id: {0} is not a participant in Workout with Id: {1}!";

            private readonly IParticipationRepository participationRepository;
            private readonly IWorkoutRepository workoutRepository;

            public DeleteParticipationCommandHandler(IParticipationRepository participationRepository, IWorkoutRepository workoutRepository)
            {
                this.participationRepository = participationRepository;
                this.workoutRepository = workoutRepository;
            }

            public async Task<Result> Handle(DeleteParticipationCommand request, CancellationToken cancellationToken)
            {
                var workout = await this.workoutRepository.ById(request.WorkoutId, cancellationToken);

                if (workout is not null)
                {
                    workout.DecrementParticipantsCount();
                    await this.workoutRepository.Save(workout, cancellationToken);
                }

                var success = await this.participationRepository.Delete(
                    request.AthleteId,
                    request.WorkoutId,
                    cancellationToken);

                if (success)
                {
                    return Result.Success;
                }

                return string.Format(NotFoundErrorMessage, request.AthleteId, request.WorkoutId);
            }
        }
    }
}
