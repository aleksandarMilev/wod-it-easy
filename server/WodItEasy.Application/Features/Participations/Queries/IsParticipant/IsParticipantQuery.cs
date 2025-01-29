namespace WodItEasy.Application.Features.Participations.Queries.IsParticipant
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;

    public class IsParticipantQuery : IRequest<bool>
    {
        public IsParticipantQuery(int athleteId, int workoutId)
        {
            this.AthleteId = athleteId;
            this.WorkoutId = workoutId;
        }

        public int AthleteId { get; set; }

        public int WorkoutId { get; set; }

        public class IsParticipantQueryHandler : IRequestHandler<IsParticipantQuery, bool>
        {
            private readonly IParticipationRepository repository;

            public IsParticipantQueryHandler(IParticipationRepository repository)
                => this.repository = repository;

            public async Task<bool> Handle(IsParticipantQuery request, CancellationToken cancellationToken)
                => await this.repository.IsParticipant(
                    request.AthleteId, 
                    request.WorkoutId, 
                    cancellationToken);
        }
    }
}
