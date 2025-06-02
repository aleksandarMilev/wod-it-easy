namespace WodItEasy.Workouts.Application.Features.Participations.Commands.Create
{
    using Athlete;
    using Commands.Common;
    using Domain.Factories.Participation;
    using Domain.Models.Athletes;
    using Domain.Models.Participation;
    using Domain.Models.Workouts;
    using MediatR;
    using Workouts;
    using WodItEasy.Common.Application;

    public class CreateParticipationCommand : IRequest<Result<ParticipationOutputModel>>
    {
        public int WorkoutId { get; set; }

        public int AthleteId { get; set; }

        public class CreateParticipationCommandHandler(
            IParticipationFactory factory,
            IParticipationRepository participationRepository,
            IWorkoutRepository workoutRepository,
            IAthleteRepository athleteRepository)
            : IRequestHandler<CreateParticipationCommand, Result<ParticipationOutputModel>>
        {
            private const string NotFoundErrorMessage = "{0} with Id: {1} not found!";
            private const string AlreadyAParticipantErrorMessage = "Athlete with Id: {0} is already a participant in workout with Id: {1}!";

            private readonly IParticipationFactory factory = factory;
            private readonly IParticipationRepository participationRepository = participationRepository;
            private readonly IWorkoutRepository workoutRepository = workoutRepository;
            private readonly IAthleteRepository athleteRepository = athleteRepository;

            public async Task<Result<ParticipationOutputModel>> Handle(
                CreateParticipationCommand request, 
                CancellationToken cancellationToken)
            {
                var athlete = await this.athleteRepository.ById(
                    request.AthleteId, 
                    cancellationToken);

                if (athlete is null)
                {
                    return string.Format(
                        NotFoundErrorMessage, 
                        nameof(Athlete), 
                        request.AthleteId);
                }

                var workout = await this.workoutRepository.ById(
                    request.WorkoutId, 
                    cancellationToken);

                if (workout is null)
                {
                    return string.Format(
                        NotFoundErrorMessage, 
                        nameof(Workout), 
                        request.WorkoutId);
                }

                var athleteIsAlreadyAParticipant = await this.participationRepository.Exists(
                    request.AthleteId, 
                    request.WorkoutId,
                    cancellationToken);

                if (athleteIsAlreadyAParticipant)
                {
                    return string.Format(
                        AlreadyAParticipantErrorMessage, 
                        request.AthleteId, 
                        request.WorkoutId);
                }

                var participation = this.factory
                    .ForAthlete(athlete)
                    .ForWorkout(workout)
                    .JoinedAt(DateTime.UtcNow)
                    .WithStatus(ParticipationStatus.Joined)
                    .Build();

                await this.participationRepository.Save(participation, cancellationToken);

                workout.IncrementParticipantsCount();

                await this.workoutRepository.Save(workout, cancellationToken);

                return new ParticipationOutputModel() { Id = participation.Id };
            }
        }
    }
}
