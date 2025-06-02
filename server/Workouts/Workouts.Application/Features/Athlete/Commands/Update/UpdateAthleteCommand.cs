namespace WodItEasy.Workouts.Application.Features.Athlete.Commands.Update
{
    using Commands.Common;
    using MediatR;
    using WodItEasy.Common.Application;
    using WodItEasy.Common.Application.Contracts;

    public class UpdateAthleteCommand
        : AthleteCommand<UpdateAthleteCommand>, IRequest<Result>
    {
        public class UpdateAthleteCommandHandler(
            IAthleteRepository repository,
            ICurrentUserService userService)
            : IRequestHandler<UpdateAthleteCommand, Result>
        {
            const string NotFoundErrorMessage = "Athlete with UserId: {0} was not found!";

            private readonly IAthleteRepository repository = repository;
            private readonly ICurrentUserService userService = userService;

            public async Task<Result> Handle(
                UpdateAthleteCommand request, 
                CancellationToken cancellationToken)
            {
                var userId = this.userService.UserId!;
                var athlete = await this.repository.ByUserId(userId, cancellationToken);

                if (athlete is null)
                {
                    return string.Format(NotFoundErrorMessage, userId);
                }

                athlete.UpdateName(request.Name);

                await this.repository.Save(athlete, cancellationToken);

                return Result.Success;
            }
        }
    }
}
