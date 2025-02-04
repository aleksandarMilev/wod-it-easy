namespace WodItEasy.Application.Features.Participations.Commands.Delete
{
    using System.Threading;
    using System.Threading.Tasks;
    using Athlete;
    using Common;
    using Contracts;
    using Domain.Models.Participation;
    using MediatR;
    using Workouts;

    public class DeleteParticipationCommand : IRequest<Result>
    {
        public DeleteParticipationCommand(int participationId)
            => this.ParticipationId = participationId;

        public int ParticipationId { get; set; }

        public class DeleteParticipationCommandHandler : IRequestHandler<DeleteParticipationCommand, Result>
        {
            private const string ParticipationNotFoundErrorMessage = "Participation with Id: {0} does not exist!";
            private const string UnauthorizedErrorMessage = "Current user can not modify this participation!";

            private readonly IParticipationRepository participationRepository;
            private readonly IAthleteRepository athleteRepository;
            private readonly IWorkoutRepository workoutRepository;
            private readonly ICurrentUserService userService;

            public DeleteParticipationCommandHandler(
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

            public async Task<Result> Handle(DeleteParticipationCommand request, CancellationToken cancellationToken)
            {
                var participation = await this.participationRepository.ById(request.ParticipationId, cancellationToken);

                if (participation is null)
                {
                    return ParticipationNotFoundErrorMessage;
                }

                var athleteId = await this.athleteRepository.GetId(this.userService.UserId!, cancellationToken);

                if (participation.AthleteId != athleteId)
                {
                    return UnauthorizedErrorMessage;
                }

                _ = await this.participationRepository.Delete(request.ParticipationId, cancellationToken);

                if (participation.Status.Equals(ParticipationStatus.Joined))
                {
                    var workout = await this.workoutRepository.ById(participation.WorkoutId, cancellationToken);

                    if (workout is not null)
                    {
                        workout.DecrementParticipantsCount();

                        await this.workoutRepository.Save(workout, cancellationToken);
                    }
                }

                return Result.Success;
            }
        }
    }
}
