namespace WodItEasy.Application.Features.Participations.Commands.Cancel
{
    using Commands.Common;
    using Features.Athlete;
    using Features.Workouts;
    using MediatR;
    using WodItEasy.Common.Application;
    using WodItEasy.Common.Application.Contracts;

    public class CancelParticipationCommand
        : ParticipationCommand<CancelParticipationCommand>, IRequest<Result>
    {
        public class CancelParticipationCommandHandler(
            IParticipationRepository participationRepository,
            IAthleteRepository athleteRepository,
            IWorkoutRepository workoutRepository,
            ICurrentUserService userService)
            : IRequestHandler<CancelParticipationCommand, Result>
        {
            private const string ParticipationNotFoundErrorMessage = "Participation with Id: {0} does not exist!";
            private const string UnauthorizedErrorMessage = "Current user can not modify this participation!";

            private readonly IParticipationRepository participationRepository = participationRepository;
            private readonly IAthleteRepository athleteRepository = athleteRepository;
            private readonly IWorkoutRepository workoutRepository = workoutRepository;
            private readonly ICurrentUserService userService = userService;

            public async Task<Result> Handle(
                CancelParticipationCommand request, 
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

                participation.MarkAsLeft();

                await this.participationRepository.Save(participation, cancellationToken);

                var workout = await this.workoutRepository.ById(
                    participation.WorkoutId, 
                    cancellationToken);

                if (workout is not null)
                {
                    workout.DecrementParticipantsCount();

                    await this.workoutRepository.Save(workout, cancellationToken);
                }

                return Result.Success;
            }
        }
    }
}
