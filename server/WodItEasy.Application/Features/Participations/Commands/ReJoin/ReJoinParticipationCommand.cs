namespace WodItEasy.Application.Features.Participations.Commands.ReJoin
{
    using System.Threading;
    using System.Threading.Tasks;
    using Common;
    using Contracts;
    using Features.Athlete;
    using Features.Workouts;
    using MediatR;

    public class ReJoinParticipationCommand : IRequest<Result<int>>
    {
        public int ParticipationId { get; set; }

        public ReJoinParticipationCommand(int participationId)
            => this.ParticipationId = participationId;

        public class ReJoinParticipationCommandHandler : IRequestHandler<ReJoinParticipationCommand, Result<int>>
        {
            private const string ParticipationNotFoundErrorMessage = "Participation with Id: {0} does not exist!";
            private const string UnauthorizedErrorMessage = "Current user can not modify this participation!";

            private readonly IParticipationRepository participationRepository;
            private readonly IAthleteRepository athleteRepository;
            private readonly IWorkoutRepository workoutRepository;
            private readonly ICurrentUserService userService;

            public ReJoinParticipationCommandHandler(
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

            public async Task<Result<int>> Handle(ReJoinParticipationCommand request, CancellationToken cancellationToken)
            {
                var participation = await this.participationRepository.ById(request.ParticipationId, cancellationToken);
                var athleteId = await this.athleteRepository.GetId(this.userService.UserId!, cancellationToken);

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

                var workout = await this.workoutRepository.ById(participation.WorkoutId, cancellationToken);

                if (workout is not null)
                {
                    workout.IncrementParticipantsCount();

                    await this.workoutRepository.Save(workout, cancellationToken);
                }

                return participation.Id;
            }
        }
    }
}
