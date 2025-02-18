namespace WodItEasy.Application.Features.Participations.Commands.Delete
{
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Common;
    using Athlete;
    using Commands.Common;
    using Contracts;
    using Domain.Models.Participation;
    using MediatR;
    using Workouts;

    public class DeleteParticipationCommand : ParticipationCommand<DeleteParticipationCommand>, IRequest<Result>
    {
        public class DeleteParticipationCommandHandler : IRequestHandler<DeleteParticipationCommand, Result>
        {
            private const string ParticipationNotFoundErrorMessage = "Participation with Id: {0} does not exist!";
            private const string UnauthorizedErrorMessage = "Current user can not modify this participation!";
            private const string WorkoutClosedErrorMessage = "You can not cancel a participation when the workout is already closed!";

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

            public async Task<Result> Handle(
                DeleteParticipationCommand request, 
                CancellationToken cancellationToken)
            {
                var participation = await this.participationRepository.ById(
                    request.Id, 
                    cancellationToken);

                if (participation is null)
                {
                    return string.Format(ParticipationNotFoundErrorMessage, request.Id);
                }

                var athleteId = await this.athleteRepository.GetId(
                    this.userService.UserId!, 
                    cancellationToken);

                if (participation.AthleteId != athleteId)
                {
                    return UnauthorizedErrorMessage;
                }

                var workout = await this.workoutRepository.ById(
                    participation.WorkoutId, 
                    cancellationToken);

                var athleteIsJoined = participation.Status.Equals(ParticipationStatus.Joined);

                if (athleteIsJoined && workout!.IsClosed())
                {
                    return WorkoutClosedErrorMessage;
                }

                _ = await this.participationRepository.Delete(request.Id, cancellationToken);

                if (athleteIsJoined)
                {
                    workout!.DecrementParticipantsCount();

                    await this.workoutRepository.Save(workout, cancellationToken);
                }

                return Result.Success;
            }
        }
    }
}
