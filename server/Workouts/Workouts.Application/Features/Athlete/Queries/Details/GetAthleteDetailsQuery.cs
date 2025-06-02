namespace WodItEasy.Workouts.Application.Features.Athlete.Queries.Details
{
    using Common.Application.Contracts;
    using MediatR;

    public class GetAthleteDetailsQuery : IRequest<GetAthleteDetailsOutputModel?>
    {
        public class GetAthleteQueryHandler(
            IAthleteRepository repository,
            ICurrentUserService userService)
            : IRequestHandler<GetAthleteDetailsQuery, GetAthleteDetailsOutputModel?>
        {
            private readonly IAthleteRepository repository = repository;
            private readonly ICurrentUserService userService = userService;

            public async Task<GetAthleteDetailsOutputModel?> Handle(
                GetAthleteDetailsQuery request, 
                CancellationToken cancellationToken)
                => await this.repository.GetOutputModel(
                    this.userService.UserId!, 
                    cancellationToken);
        }
    }
}
