namespace WodItEasy.Application.Features.Athlete.Commands.Delete
{
    using System.Threading;
    using System.Threading.Tasks;
    using Common;
    using Contracts;
    using MediatR;

    public class DeleteAthleteCommand : IRequest<Result>
    {
        public class DeleteAthleteCommandHandler : IRequestHandler<DeleteAthleteCommand, Result>
        {
            private const string NotFoundErrorMessage = "Athlete with UserId: {0} was not found!";

            private readonly IAthleteRepository repository;
            private readonly ICurrentUserService userService;

            public DeleteAthleteCommandHandler(IAthleteRepository repository, ICurrentUserService userService)
            {
                this.repository = repository;
                this.userService = userService;
            }

            public async Task<Result> Handle(DeleteAthleteCommand request, CancellationToken cancellationToken)
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
