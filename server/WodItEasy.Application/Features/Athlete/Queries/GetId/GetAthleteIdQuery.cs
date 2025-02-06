namespace WodItEasy.Application.Features.Athlete.Queries.GetId
{
    using System.Threading;
    using System.Threading.Tasks;
    using Contracts;
    using MediatR;

    public class GetAthleteIdQuery : IRequest<GetAthleteIdOutputModel?>
    {
        public class GetAthleteIdQueryHandler : IRequestHandler<GetAthleteIdQuery, GetAthleteIdOutputModel?>
        {
            private readonly IAthleteRepository repository;
            private readonly ICurrentUserService userService;

            public GetAthleteIdQueryHandler(IAthleteRepository repository, ICurrentUserService userService)
            {
                this.repository = repository;
                this.userService = userService;
            }

            public async Task<GetAthleteIdOutputModel?> Handle(GetAthleteIdQuery request, CancellationToken cancellationToken)
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
