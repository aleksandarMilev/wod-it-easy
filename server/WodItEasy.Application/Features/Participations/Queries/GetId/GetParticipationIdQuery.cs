namespace WodItEasy.Application.Features.Participations.Queries.GetId
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;

    public class GetParticipationIdQuery : IRequest<GetParticipationIdOutputModel>
    {
        public GetParticipationIdQuery(int athleteId, int workoutId)
        {
            this.AthleteId = athleteId;
            this.WorkoutId = workoutId;
        }

        public int AthleteId { get; set; }

        public int WorkoutId { get; set; }

        public class GetParticipationIdQueryHandler : IRequestHandler<GetParticipationIdQuery, GetParticipationIdOutputModel>
        {
            private readonly IParticipationRepository repository;

            public GetParticipationIdQueryHandler(IParticipationRepository repository)
                => this.repository = repository;

            public async Task<GetParticipationIdOutputModel> Handle(GetParticipationIdQuery request, CancellationToken cancellationToken)
            {
                var id = await this.repository.GetId(
                    request.AthleteId,
                    request.WorkoutId,
                    cancellationToken);

                return new GetParticipationIdOutputModel() { Id = id };
            }
        }
    }
}
