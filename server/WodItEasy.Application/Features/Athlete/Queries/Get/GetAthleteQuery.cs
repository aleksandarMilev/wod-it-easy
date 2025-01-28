namespace WodItEasy.Application.Features.Athlete.Queries.Get
{
    using System.Threading;
    using System.Threading.Tasks;
    using Contracts;
    using MediatR;

    public class GetAthleteQuery : IRequest<GetAthleteOutputModel?>
    {
        public class GetAthleteQueryHandler : IRequestHandler<GetAthleteQuery, GetAthleteOutputModel?>
        {
            private readonly IAthleteRepository repository;
            private readonly ICurrentUserService userService;

            public GetAthleteQueryHandler(IAthleteRepository repository, ICurrentUserService userService)
            {
                this.repository = repository;
                this.userService = userService;
            }

            public async Task<GetAthleteOutputModel?> Handle(GetAthleteQuery request, CancellationToken cancellationToken)
                => await this.repository.GetOutputModel(this.userService.UserId!, cancellationToken);
        }
    }
}
