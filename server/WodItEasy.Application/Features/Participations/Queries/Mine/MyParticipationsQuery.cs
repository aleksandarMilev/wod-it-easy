namespace WodItEasy.Application.Features.Participations.Queries.Mine
{
    using System.Threading;
    using System.Threading.Tasks;
    using Athlete;
    using Common;
    using Contracts;
    using Domain.Exceptions;
    using MediatR;

    using static Common.DefaultValues;

    public class MyParticipationsQuery : IRequest<PaginatedOutputModel<MyParticipationsOutputModel>>
    {
        public int PageIndex { get; private set; } = DefaultPageIndex;

        public int PageSize { get; private set; } = DefaultPageSize;

        public class MyParticipationsQueryHandler : IRequestHandler<MyParticipationsQuery, PaginatedOutputModel<MyParticipationsOutputModel>>
        {
            private readonly IParticipationRepository participationRepository;
            private readonly IAthleteRepository athleteRepository;
            private readonly ICurrentUserService userService;

            public MyParticipationsQueryHandler(
                IParticipationRepository participationRepository, 
                IAthleteRepository athleteRepository, 
                ICurrentUserService userService)
            {
                this.participationRepository = participationRepository;
                this.athleteRepository = athleteRepository;
                this.userService = userService;
            }

            public async Task<PaginatedOutputModel<MyParticipationsOutputModel>> Handle(MyParticipationsQuery request, CancellationToken cancellationToken)
            {
                var athleteId = await this.athleteRepository
                    .GetId(this.userService.UserId!, cancellationToken)
                    ?? throw new InvalidAthleteException("This user is not an athlete!");

                return await this.participationRepository.Mine(
                    athleteId,
                    request.PageIndex,
                    request.PageSize,
                    cancellationToken);
            }
        }
    }
}
