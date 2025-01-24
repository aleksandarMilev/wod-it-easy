namespace WodItEasy.Application.Features.Participations.Commands.Delete
{
    using System.Threading;
    using System.Threading.Tasks;
    using Common;
    using MediatR;

    public class DeleteParticipationCommand : IRequest<Result>
    {
        private const string NotFoundErrorMessage = "Athlete with Id: {0} is not a participant in Workout with Id: {1}!";

        public int WorkoutId { get; set; }

        public int AthleteId { get; set; }

        public class DeleteParticipationCommandHandler : IRequestHandler<DeleteParticipationCommand, Result>
        {
            private readonly IParticipationRepository repository;

            public DeleteParticipationCommandHandler(IParticipationRepository repository) 
                => this.repository = repository;

            public async Task<Result> Handle(DeleteParticipationCommand request, CancellationToken cancellationToken)
            {
                var success = await this.repository.Delete(
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
