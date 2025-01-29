namespace WodItEasy.Application.Features.Participations.Commands.Create
{
    using System.Threading;
    using System.Threading.Tasks;
    using Athlete;
    using Common;
    using Domain.Factories.Participation;
    using Domain.Models.Athletes;
    using Domain.Models.Workouts;
    using MediatR;
    using Workouts;

    public class CreateParticipationCommand : IRequest<Result>
    {
        private const string NotFoundErrorMessage = "{0} with Id: {1} not found!";

        public int WorkoutId { get; set; }

        public int AthleteId { get; set; }

        public class CreateParticipationCommandHandler : IRequestHandler<CreateParticipationCommand, Result>
        {
            private readonly IParticipationFactory factory;
            private readonly IParticipationRepository participationRepository;
            private readonly IWorkoutRepository workoutRepository;
            private readonly IAthleteRepository athleteRepository;

            public CreateParticipationCommandHandler(
                IParticipationFactory factory,
                IParticipationRepository participationRepository, 
                IWorkoutRepository workoutRepository, 
                IAthleteRepository athleteRepository)
            {
                this.factory = factory;
                this.participationRepository = participationRepository;
                this.workoutRepository = workoutRepository;
                this.athleteRepository = athleteRepository;
            }

            public async Task<Result> Handle(CreateParticipationCommand request, CancellationToken cancellationToken)
            {
                var athleteExists = await this.athleteRepository.ExistsById(request.AthleteId, cancellationToken);

                if (!athleteExists)
                {
                    return string.Format(NotFoundErrorMessage, nameof(Athlete), request.AthleteId);
                }

                var workoutExists = await this.workoutRepository.ExistsById(request.WorkoutId, cancellationToken);

                if (!workoutExists)
                {
                    return string.Format(NotFoundErrorMessage, nameof(Workout), request.WorkoutId);
                }

                var participation = this.factory
                    .WithAthleteId(request.AthleteId)
                    .WithWorkoutId(request.WorkoutId)
                    .Build();

                await participationRepository.Save(participation, cancellationToken);

                return Result.Success;
            }
        }
    }
}
