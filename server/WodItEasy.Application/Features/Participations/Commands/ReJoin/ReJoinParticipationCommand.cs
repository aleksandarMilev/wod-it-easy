namespace WodItEasy.Application.Features.Participations.Commands.ReJoin
{
    using Commands.Common;
    using Features.Athlete;
    using Features.Workouts;
    using MediatR;
    using WodItEasy.Common.Application;
    using WodItEasy.Common.Application.Contracts;

    public class ReJoinParticipationCommand
        : ParticipationCommand<ReJoinParticipationCommand>, IRequest<Result<ParticipationOutputModel>>
    {
        public class ReJoinParticipationCommandHandler(
            IParticipationRepository participationRepository,
            IAthleteRepository athleteRepository,
            IWorkoutRepository workoutRepository,
            ICurrentUserService userService)
            : IRequestHandler<ReJoinParticipationCommand, Result<ParticipationOutputModel>>
        {
            private const string ParticipationNotFoundErrorMessage = "Participation with Id: {0} does not exist!";
            private const string UnauthorizedErrorMessage = "Current user can not modify this participation!";
            private const string WorkoutFullErrorMessage = "This workout has reached the max participants count ({0})!";

            private readonly IParticipationRepository participationRepository = participationRepository;
            private readonly IAthleteRepository athleteRepository = athleteRepository;
            private readonly IWorkoutRepository workoutRepository = workoutRepository;
            private readonly ICurrentUserService userService = userService;

            public async Task<Result<ParticipationOutputModel>> Handle(
                ReJoinParticipationCommand request, 
                CancellationToken cancellationToken)
            {
                var participation = await this.participationRepository.ById(
                    request.Id, 
                    cancellationToken);

                var athleteId = await this.athleteRepository.GetId(
                    this.userService.UserId!, 
                    cancellationToken);

                if (participation is null)
                {
                    return ParticipationNotFoundErrorMessage;
                }

                if (participation.AthleteId != athleteId)
                {
                    return UnauthorizedErrorMessage;
                }

                participation.MarkAsJoined();

                await this.participationRepository.Save(participation, cancellationToken);

                var workout = await this.workoutRepository.ById(
                    participation.WorkoutId, 
                    cancellationToken);

                if (workout is not null)
                {
                    if (workout.CurrentParticipantsCount == workout.MaxParticipantsCount)
                    {
                        return string.Format(WorkoutFullErrorMessage, workout.CurrentParticipantsCount);
                    }

                    workout.IncrementParticipantsCount();

                    await this.workoutRepository.Save(workout, cancellationToken);
                }

                return new ParticipationOutputModel() { Id = participation.Id };
            }
        }
    }
}
