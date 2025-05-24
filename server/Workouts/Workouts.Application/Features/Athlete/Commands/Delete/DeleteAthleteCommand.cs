namespace WodItEasy.Workouts.Application.Features.Athlete.Commands.Delete
{
    using MediatR;
    using WodItEasy.Common.Application;
    using WodItEasy.Common.Application.Contracts;

    public class DeleteAthleteCommand : IRequest<Result>
    {
        public class DeleteAthleteCommandHandler(
            IAthleteRepository repository,
            ICurrentUserService userService)
            : IRequestHandler<DeleteAthleteCommand, Result>
        {
            private const string NotFoundErrorMessage = "Athlete with UserId: {0} was not found!";

            private readonly IAthleteRepository repository = repository;
            private readonly ICurrentUserService userService = userService;

            public async Task<Result> Handle(
                DeleteAthleteCommand request, 
                CancellationToken cancellationToken)
            {
                var userId = this.userService.UserId!;
                var success = await this.repository.Delete(userId, cancellationToken);

                if (success)
                {
                    return Result.Success;
                }

                return string.Format(NotFoundErrorMessage, userId);
            }
        }
    }
}
