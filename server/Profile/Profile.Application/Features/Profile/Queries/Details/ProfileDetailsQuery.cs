namespace WodItEasy.Profile.Application.Features.Profile.Queries.Details
{
    using MediatR;
    using Common.Application.Queries;
    using WodItEasy.Common.Application.Contracts;

    public class ProfileDetailsQuery
        : EntityQuery<int>, IRequest<ProfileDetailsOutputModel?>
    {
        public class WorkoutDetailsQueryHandler(
            ICurrentUserService userService,
            IProfileRepository repository)
            : IRequestHandler<ProfileDetailsQuery, ProfileDetailsOutputModel?>
        {
            private readonly ICurrentUserService userService = userService;
            private readonly IProfileRepository repository = repository;

            public Task<ProfileDetailsOutputModel?> Handle(
                ProfileDetailsQuery request,
                CancellationToken cancellationToken)
                => this.repository.Details(
                    this.userService.UserId!,
                    cancellationToken);
        }
    }
}
