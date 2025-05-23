namespace WodItEasy.Application.Features.Athlete.Queries.GetId
{
    using MediatR;
    using WodItEasy.Common.Application.Contracts;

    public class GetAthleteIdQuery : IRequest<GetAthleteIdOutputModel?>
    {
        public class GetAthleteIdQueryHandler(
            IAthleteRepository repository,
            ICurrentUserService userService)
            : IRequestHandler<GetAthleteIdQuery, GetAthleteIdOutputModel?>
        {
            private readonly IAthleteRepository repository = repository;
            private readonly ICurrentUserService userService = userService;

            public async Task<GetAthleteIdOutputModel?> Handle(
                GetAthleteIdQuery request, 
                CancellationToken cancellationToken)
            {
                var id = await this.repository.GetId(
                    this.userService.UserId!,
                    cancellationToken);

                if (id is null)
                {
                    return null;
                }

                return new GetAthleteIdOutputModel() { Id = id.Value };
            }
        }
    }
}
