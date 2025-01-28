namespace WodItEasy.Application.Features.Athlete.Queries.GetId
{
    using System.Threading;
    using System.Threading.Tasks;
    using Contracts;
    using MediatR;

    public class GetAthleteIdQuery : IRequest<int?>
    {
        public class GetAthleteIdQueryHandler : IRequestHandler<GetAthleteIdQuery, int?>
        {
            private readonly IAthleteRepository repository;
            private readonly ICurrentUserService userService;

            public GetAthleteIdQueryHandler(IAthleteRepository repository, ICurrentUserService userService)
            {
                this.repository = repository;
                this.userService = userService;
            }

            public async Task<int?> Handle(GetAthleteIdQuery request, CancellationToken cancellationToken)
                => await this.repository.GetId(
                    this.userService.UserId!,
                    cancellationToken);
        }
    }
}
