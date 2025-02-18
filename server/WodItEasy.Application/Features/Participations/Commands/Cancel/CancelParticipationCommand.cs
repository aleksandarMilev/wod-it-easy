namespace WodItEasy.Application.Features.Participations.Commands.Cancel
{
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Common;
    using Commands.Common;
    using Contracts;
    using Features.Athlete;
    using Features.Workouts;
    using MediatR;

    public class CancelParticipationCommand : ParticipationCommand<CancelParticipationCommand>, IRequest<Result>
    {
        public class CancelParticipationCommandHandler : IRequestHandler<CancelParticipationCommand, Result>
        {
            private const string ParticipationNotFoundErrorMessage = "Participation with Id: {0} does not exist!";
            private const string UnauthorizedErrorMessage = "Current user can not modify this participation!";

            private readonly IParticipationRepository participationRepository;
            private readonly IAthleteRepository athleteRepository;
            private readonly IWorkoutRepository workoutRepository;
            private readonly ICurrentUserService userService;

            public CancelParticipationCommandHandler(
                IParticipationRepository participationRepository, 
                IAthleteRepository athleteRepository,
                IWorkoutRepository workoutRepository,
                ICurrentUserService userService)
            {
                this.participationRepository = participationRepository;
                this.athleteRepository = athleteRepository;
                this.workoutRepository = workoutRepository;
                this.userService = userService;
            }

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
