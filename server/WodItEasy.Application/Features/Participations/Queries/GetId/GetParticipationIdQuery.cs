namespace WodItEasy.Application.Features.Participations.Queries.GetId
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;

    public class GetParticipationIdQuery : IRequest<int>
    {
        public GetParticipationIdQuery(int athleteId, int workoutId)
        {
            this.AthleteId = athleteId;
            this.WorkoutId = workoutId;
        }

        public int AthleteId { get; private set; }

        public int WorkoutId { get; private set; }

        public class GetParticipationIdQueryHandler : IRequestHandler<GetParticipationIdQuery, int>
        {
            private readonly IParticipationRepository repository;

            public GetParticipationIdQueryHandler(IParticipationRepository repository)
                => this.repository = repository;

            public async Task<int> Handle(GetParticipationIdQuery request, CancellationToken cancellationToken)
                => await this.repository.GetId(
                    request.AthleteId, 
                    request.WorkoutId, 
                    cancellationToken);
        }
    }
}
