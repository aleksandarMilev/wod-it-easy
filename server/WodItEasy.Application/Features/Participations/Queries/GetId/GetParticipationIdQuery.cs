namespace WodItEasy.Application.Features.Participations.Queries.GetId
{
    using MediatR;

    public class GetParticipationIdQuery : IRequest<GetParticipationIdOutputModel>
    {
        public int AthleteId { get; set; }

        public int WorkoutId { get; set; }

        public class GetParticipationIdQueryHandler(IParticipationRepository repository)
            : IRequestHandler<GetParticipationIdQuery, GetParticipationIdOutputModel>
        {
            private readonly IParticipationRepository repository = repository;

            public async Task<GetParticipationIdOutputModel> Handle(
                GetParticipationIdQuery request, 
                CancellationToken cancellationToken)
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
