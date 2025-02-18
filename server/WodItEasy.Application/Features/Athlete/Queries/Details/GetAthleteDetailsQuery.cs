namespace WodItEasy.Application.Features.Athlete.Queries.Details
{
    using System.Threading;
    using System.Threading.Tasks;
    using Contracts;
    using MediatR;

    public class GetAthleteDetailsQuery : IRequest<GetAthleteDetailsOutputModel?>
    {
        public class GetAthleteQueryHandler : IRequestHandler<GetAthleteDetailsQuery, GetAthleteDetailsOutputModel?>
        {
            private readonly IAthleteRepository repository;
            private readonly ICurrentUserService userService;

            public GetAthleteQueryHandler(
                IAthleteRepository repository, 
                ICurrentUserService userService)
            {
                this.repository = repository;
                this.userService = userService;
            }

            public async Task<GetAthleteDetailsOutputModel?> Handle(
                GetAthleteDetailsQuery request, 
                CancellationToken cancellationToken)
                => await this.repository.GetOutputModel(
                    this.userService.UserId!, 
                    cancellationToken);
        }
    }
}
