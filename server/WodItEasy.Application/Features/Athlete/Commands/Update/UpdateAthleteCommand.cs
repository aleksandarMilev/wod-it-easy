namespace WodItEasy.Application.Features.Athlete.Commands.Update
{
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Common;
    using Contracts;
    using MediatR;

    public class UpdateAthleteCommand : IRequest<Result>
    {
        public string Name { get; private set; } = null!;

        public class UpdateAthleteCommandHandler : IRequestHandler<UpdateAthleteCommand, Result>
        {
            const string NotFoundErrorMessage = "Athlete with UserId: {0} was not found!";

            private readonly IAthleteRepository repository;
            private readonly ICurrentUserService userService;

            public UpdateAthleteCommandHandler(IAthleteRepository repository, ICurrentUserService userService)
            {
                this.repository = repository;
                this.userService = userService;
            }

            public async Task<Result> Handle(UpdateAthleteCommand request, CancellationToken cancellationToken)
            {
                var userId = this.userService.UserId!;
                var athlete = await this.repository.ByUserId(userId, cancellationToken);

                if (athlete is null)
                {
                    return string.Format(NotFoundErrorMessage, userId);
                }

                athlete.UpdateName(request.Name);

                await repository.Save(athlete, cancellationToken);

                return Result.Success;
            }
        }
    }
}
