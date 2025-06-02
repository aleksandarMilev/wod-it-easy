namespace WodItEasy.Workouts.Application.Features.Participations.Queries.Mine
{
    using Athlete;
    using Domain.Exceptions;
    using MediatR;
    using WodItEasy.Common.Application.Contracts;
    using WodItEasy.Common.Application.Models;

    using static WodItEasy.Common.Application.Constants.DefaultValues;


    public class MyParticipationsQuery
        : IRequest<PaginatedOutputModel<MyParticipationsOutputModel>>
    {
        public int PageIndex { get; set; } = DefaultPageIndex;

        public int PageSize { get; set; } = DefaultPageSize;

        public class MyParticipationsQueryHandler(
            IParticipationRepository participationRepository,
            IAthleteRepository athleteRepository,
            ICurrentUserService userService)
            : IRequestHandler<MyParticipationsQuery, PaginatedOutputModel<MyParticipationsOutputModel>>
        {
            private readonly IParticipationRepository participationRepository = participationRepository;
            private readonly IAthleteRepository athleteRepository = athleteRepository;
            private readonly ICurrentUserService userService = userService;

            public async Task<PaginatedOutputModel<MyParticipationsOutputModel>> Handle(
                MyParticipationsQuery request, 
                CancellationToken cancellationToken)
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
